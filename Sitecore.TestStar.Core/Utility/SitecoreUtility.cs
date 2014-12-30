using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.SecurityModel;
using Sitecore.TestStar.Core.Extensions;
using Cons = Sitecore.TestStar.Core.Utility.Constants;

namespace Sitecore.TestStar.Core.Utility {
	public static class SitecoreUtility {

		public static Sitecore.Data.ID GetID(string id){
			return (Sitecore.Data.ID.IsID(id))
				? Sitecore.Data.ID.Parse(id)
				: Sitecore.Data.ID.Null;
		}

		public static void AddUnitTestResults(string testName, string message) {
		}

		public static void AddWebTestResults(string testName, string url, HttpStatusCode statusCode, string message) {

			StringBuilder fullText = new StringBuilder();
			StringBuilder descText = new StringBuilder();

			//short text
			descText.AppendFormat("<div class='resultLink'>{0}: {1}</div>", GetKeyStr(((int)statusCode).ToString()), MakeHref(url));

			//full text
			fullText.Append("<div class='resultError'>");
			fullText.AppendFormat("{0}: {1}<br/>", GetKeyStr("Time"), DateTime.Now.ToString("h:mm tt"));
			fullText.AppendFormat("{0}: {1}<br/>", GetKeyStr("URL"), MakeHref(url));
			fullText.AppendFormat("{0}: {1} - {2}<br/>", GetKeyStr("Status Code"), ((int)statusCode).ToString(), statusCode.ToString());
			fullText.AppendFormat("{0}: {1}<br/>", GetKeyStr("Body"), message);
			fullText.Append("</div>");

			//if there are error then create a blog post
			if (fullText.Length > 0) {

				DateTime postDate = DateTime.Now;
				string postDescription = descText.ToString();
				string postText = fullText.ToString();
				string postItemName = GetItemName(testName);

				//change to the item in the master db so that content isn't created in the web db
				Item resultsFolder = Cons.MasterDB.GetItem(Cons.ResultsFolder);
				//need to open security to create item
				using (new SecurityDisabler()) {
					//get date folder
					Sitecore.Data.Items.TemplateItem folderTemplate = Cons.MasterDB.Templates[Cons.ResultsFolderTemplate];
					Item parentNode = GetDateParentNode(resultsFolder, postDate, folderTemplate);
					if (parentNode != null) {
						//see if post already exists
						Item newItem = parentNode.Axes.GetChild(postItemName);
						//if it doesn't exist then create it
						if (newItem == null) {
							//Create new item
							Sitecore.Data.Items.TemplateItem newTemp = Cons.MasterDB.Templates[Cons.ResultsTemplate];
							newItem = parentNode.Add(postItemName, newTemp);
						}
						//if you created the item successfully
						if (newItem != null) {
							//set permissions to edit comment data fields
							using (new EditContext(newItem, true, false)) {
								newItem["Title"] = testName;
								newItem["Date"] = postDate.ToDateFieldValue();
								newItem["Description"] += postDescription;
								newItem["Message"] += postText;
							}
							PublishItem(newItem);
						}
					}
				}
			}
		}

		public static string GetItemName(string val) {

			string newVal = ItemUtil.ProposeValidItemName(val);
			
			StringBuilder sb = new StringBuilder();
			Dictionary<char, char> invalid = Sitecore.Configuration.Settings.InvalidItemNameChars.ToDictionary<char, char>(a => a);
			foreach (char c in newVal) {
				if (!invalid.ContainsKey(c))
					sb.Append(c);
			}

			return sb.ToString().Trim();
		}

		public static string MakeHref(string a) {
			return string.Format("<a href='{0}' target='_blank'>{0}</a>", a);
		}

		public static string GetKeyStr(string s) {
			return string.Format("<span class='resultKey'>{0}</span>", s);
		}

		public static Item GetDateParentNode(Item parentNode, DateTime dt, TemplateItem folderType) {

			//get year folder
			Item year = parentNode.Children[dt.Year.ToString()];
			if (year == null) {
				//build year folder if you have to
				year = parentNode.Add(dt.Year.ToString(), folderType);
				PublishItem(year);
			}
			//set the parent to year
			parentNode = year;

			//get month folder
			Item month = parentNode.Children[dt.ToString("MM")];
			if (month == null) {
				//build month folder if you have to
				month = parentNode.Add(dt.ToString("MM"), folderType);
				PublishItem(month);
			}
			//set the parent to year
			parentNode = month;

			//get day folder
			Item day = parentNode.Children[dt.ToString("dd")];
			if (day == null) {
				//build day folder if you have to
				day = parentNode.Add(dt.ToString("dd"), folderType);
				PublishItem(day);
			}
			//set the parent to year
			parentNode = day;

			return parentNode;
		}

		public static void PublishItem(Item item) {

			var master = Sitecore.Configuration.Factory.GetDatabase("master");
			var targetDB = Sitecore.Configuration.Factory.GetDatabase("web");

			DateTime publishDate = DateTime.Now;
			var pubOpts = new Sitecore.Publishing.PublishOptions(master, targetDB, Sitecore.Publishing.PublishMode.Full, Sitecore.Data.Managers.LanguageManager.DefaultLanguage, publishDate);
			pubOpts.Deep = false;
			pubOpts.RootItem = item;
			var pub = new Sitecore.Publishing.Publisher(pubOpts);
			Sitecore.Jobs.Job pubJob = pub.PublishAsync();
			pubJob.Start();
		}
	}
}
