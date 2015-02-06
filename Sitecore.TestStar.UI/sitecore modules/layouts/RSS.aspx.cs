using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data.Items;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Extensions;
using Sitecore.TestStar.Core.Providers;
using Sitecore.TestStar.Core.Providers.Interfaces;

namespace Sitecore.TestStar.Core.UI.layouts {
	public partial class RSS : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {

			//write out xml and output message
			Response.StatusCode = 200;
			Response.ContentType = "application/rss+xml";
			XmlOutput.Text = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";

			//handle the recent posts
            ITestResultProvider tProvider = (ITestResultProvider)new SCTestResultProvider();
			List<TestResultList> posts = tProvider.GetResults().ToList();
			if (posts.Count > 20) 
				posts = posts.GetRange(0, 20);
			rptRSS.DataSource = posts;
			rptRSS.DataBind();
		}

		protected void rptRSS_ItemDataBound(object sender, RepeaterItemEventArgs e) {
			if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;

			TestResultList r = (TestResultList)e.Item.DataItem;

			//get the entry children
			Item rItem = Sitecore.Context.Database.GetItemByID(r.ID);
			Repeater rptEntries = (Repeater)e.Item.FindControl("rptEntries");
			rptEntries.DataSource = rItem.GetChildren().Reverse();
			rptEntries.DataBind();
		}

		protected void rptEntries_ItemDataBound(object sender, RepeaterItemEventArgs e) {
			if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;

			//show extra information for web test items
			Item entry = (Item)e.Item.DataItem;
			if (entry.TemplateID.ToString().Equals(Sitecore.TestStar.Core.Utility.Constants.WebTestResultTemplate)) {
				Literal ltlWebTestDetails = (Literal)e.Item.FindControl("ltlWebTestDetails");
				ltlWebTestDetails.Text = string.Format("{0} - {1}: {2}<br/>{3}", entry["Site"], entry["Environment"], entry["ResponseStatus"], entry["RequestURL"]);
			}
		}

		protected string FixDescription(string description) {
			string url = "https://" + Request.Url.Host + "/";
			string newString = description.Replace("href=\"/", "href=\"" + url).Replace("src=\"/", "src=\"" + url).Replace("src=\"~/", "src=\"" + url + "~/");

			return "<![CDATA[" + newString + "]]>";
		}
	}
}