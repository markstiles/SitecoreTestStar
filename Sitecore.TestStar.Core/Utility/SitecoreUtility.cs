using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data;
using Sitecore.Data.Fields;
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

		#region Result Definition

		public static void AddUnitTestResults(string testName, string message) {
			
			StringBuilder resultMessage = new StringBuilder();
			resultMessage.Append("<div class='resultError'>");
			resultMessage.AppendFormat("{0}: {1}<br/>", GetKeyStr("Time"), DateTime.Now.ToString("h:mm tt"));
			resultMessage.AppendFormat("{0}: {1}<br/>", GetKeyStr("Body"), message);
			resultMessage.Append("</div>");

			CreateResult(GetItemName(testName), testName, message, resultMessage.ToString(), true);
		}

		public static void AddWebTestResults(string testName, string url, HttpStatusCode statusCode, string message) {

			//short text
			StringBuilder resultDesc = new StringBuilder();
			resultDesc.AppendFormat("<div class='resultLink'>{0}: {1}</div>", GetKeyStr(((int)statusCode).ToString()), MakeHref(url));

			//full text
			StringBuilder resultMessage = new StringBuilder();
			resultMessage.Append("<div class='resultError'>");
			resultMessage.AppendFormat("{0}: {1}<br/>", GetKeyStr("Time"), DateTime.Now.ToString("h:mm tt"));
			resultMessage.AppendFormat("{0}: {1}<br/>", GetKeyStr("URL"), MakeHref(url));
			resultMessage.AppendFormat("{0}: {1} - {2}<br/>", GetKeyStr("Status Code"), ((int)statusCode).ToString(), statusCode.ToString());
			resultMessage.AppendFormat("{0}: {1}<br/>", GetKeyStr("Body"), message);
			resultMessage.Append("</div>");

			CreateResult(GetItemName(testName), testName, resultDesc.ToString(), resultMessage.ToString(), false);
		}

		#endregion Result Definition

		#region Helper Methods

		public static void CreateResult(string resultItemName, string title, string desc, string message, bool isUnitTest){
			//change to the item in the master db so that content isn't created in the web db
			Item resultsFolder = Cons.MasterDB.GetItem(Cons.ResultsFolder);
			//need to open security to create item
			using (new SecurityDisabler()) {
				//get date folder
				Sitecore.Data.Items.TemplateItem folderTemplate = Cons.MasterDB.Templates[Cons.ResultsFolderTemplate];
				Item parentNode = GetDateParentNode(resultsFolder, DateTime.Now, folderTemplate);
				if (parentNode == null) 
					return;

				//see if post already exists
				Item newItem = GetResult(parentNode, resultItemName, isUnitTest);
				
				//if you created the item successfully
				if (newItem == null) 
					return;

				//set permissions to edit comment data fields
				using (new EditContext(newItem, true, false)) {
					newItem["Title"] = title;
					newItem["Date"] = DateTime.Now.ToDateFieldValue();
					newItem["Description"] += desc;
					newItem["Message"] += message;
					newItem["IsUnitTest"] = (isUnitTest) ? "1" : string.Empty;
				}
				PublishItem(newItem);
			}
		}

		public static Item GetResult(Item parentNode, string resultName, bool isUnitTest) {
			//see if post already exists
			Item newItem = parentNode.Axes.GetChild(resultName);
			//separate web tests from unit test results
			bool isUnitResult = (newItem != null && newItem.Fields["IsUnitTest"] != null && ((CheckboxField)newItem.Fields["IsUnitTest"]).Checked);
			//if it doesn't exist or if it's not the same type we want, then create it
			if (newItem == null || isUnitTest != isUnitResult) {
				//Create new item
				Sitecore.Data.Items.TemplateItem newTemp = Cons.MasterDB.Templates[Cons.ResultsTemplate];
				newItem = parentNode.Add(resultName, newTemp);
			}
			return newItem;
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

			var pubOpts = new Sitecore.Publishing.PublishOptions(
				Cons.MasterDB, 
				Cons.WebDB, 
				Sitecore.Publishing.PublishMode.Full, 
				Sitecore.Data.Managers.LanguageManager.DefaultLanguage, 
				DateTime.Now);
			pubOpts.Deep = false;
			pubOpts.RootItem = item;
			var pub = new Sitecore.Publishing.Publisher(pubOpts);
			Sitecore.Jobs.Job pubJob = pub.PublishAsync();
			pubJob.Start();
		}

		#endregion Helper Methods

		#region Text Formatting

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

		#endregion Text Formatting
	}
}
