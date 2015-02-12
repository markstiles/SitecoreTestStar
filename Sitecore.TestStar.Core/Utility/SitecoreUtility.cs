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
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Extensions;
using Cons = Sitecore.TestStar.Core.Utility.Constants;
using Sitecore.Configuration;

namespace Sitecore.TestStar.Core.Utility {
	public static class SitecoreUtility {

		public static Sitecore.Data.ID GetID(string id){
			return (Sitecore.Data.ID.IsID(id))
				? Sitecore.Data.ID.Parse(id)
				: Sitecore.Data.ID.Null;
		}

		#region Result Definition

        public static string CreateResultEntry(DefaultUnitTestResult d) {
            return CreateResultEntry(d.ListName, d.Date.ToDateFieldValue(), d.ClassName, d.Method, d.Type, d.Message, true, string.Empty, string.Empty, string.Empty, string.Empty);
        }

        public static string CreateResultEntry(DefaultWebTestResult d) {
            return CreateResultEntry(d.ListName, d.Date.ToDateFieldValue(), d.ClassName, d.Method, d.Type, d.Message, false, d.Site, d.Environment, d.RequestURL, d.ResponseStatus);
        }

		private static string CreateResultEntry(string listName, string dateValue, string className, string method, string type, string message, bool isUnitTest, string siteID, string envID, string url, string status) {
			string returnID = string.Empty;
			//change to the item in the master db so that content isn't created in the web db
			Item resultsFolder = Cons.MasterDB.GetItem(Settings.GetSetting("TestStar.ResultsFolder"));
			//need to open security to create item
			using (new SecurityDisabler()) {
				//get date folder
                Sitecore.Data.Items.TemplateItem folderTemplate = Cons.MasterDB.Templates[Settings.GetSetting("TestStar.ResultsFolderTemplate")];
				Item parentNode = GetDateParentNode(resultsFolder, DateTime.Now, folderTemplate);
				if (parentNode == null) 
					return returnID;

				string goodListName = GetItemName(listName);
				//see if post already exists
				Item listItem = parentNode.Axes.GetChild(goodListName);
				//if it doesn't exist or if it's not the same type we want, then create it
				if (listItem == null) //Create new item
                    listItem = parentNode.Add(goodListName, Cons.MasterDB.Templates[Settings.GetSetting("TestStar.ResultsListTemplate")]);

				//if you created the item successfully
				if (listItem == null)
					return returnID;

				//set permissions to edit comment data fields
				if (listItem.Fields["Date"] == null || listItem["Date"].Equals(string.Empty)) {
					using (new EditContext(listItem, true, false)) {
						listItem["Date"] = DateTime.Now.ToDateFieldValue();
					}
				}
			
				string entryName = (listItem.Children.Count + 1).ToString("0000");
                Item newEntryItem = listItem.Add(entryName, Cons.MasterDB.Templates[(isUnitTest) ? Settings.GetSetting("TestStar.UnitTestResultTemplate") : Settings.GetSetting("TestStar.WebTestResultTemplate")]);
				returnID = newEntryItem.ID.ToString();
				using (new EditContext(newEntryItem, true, false)) {
					newEntryItem["Type"] = type;
					newEntryItem["Method"] = method;
					newEntryItem["ClassName"] = className;
					newEntryItem["Message"] = message;
					newEntryItem["Date"] = dateValue;

					if (!isUnitTest) {
						newEntryItem["Site"] = siteID;
						newEntryItem["Environment"] = envID;
						newEntryItem["RequestURL"] = url;
						newEntryItem["ResponseStatus"] = status;
					}
				}

				PublishItem(listItem);
			}
			return returnID;
		}

		#endregion Result Definition

		#region Helper Methods

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
			pubOpts.Deep = true;
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
