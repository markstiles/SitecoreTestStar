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
using Sitecore.TestStar.Core.Entities.Interfaces;

namespace Sitecore.TestStar.Core.UI.layouts {
	public partial class RSS : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {

			//write out xml and output message
			Response.StatusCode = 200;
			Response.ContentType = "application/rss+xml";
			XmlOutput.Text = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";

			//handle the recent posts
            ITestResultProvider tProvider = (ITestResultProvider)new SCTestResultProvider();
            List<ITestResultList> posts = tProvider.GetTestResultLists().ToList();
			if (posts.Count > 20) 
				posts = posts.GetRange(0, 20);
			rptRSS.DataSource = posts;
			rptRSS.DataBind();
		}

		protected void rptRSS_ItemDataBound(object sender, RepeaterItemEventArgs e) {
			if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;

			ITestResultList r = (ITestResultList)e.Item.DataItem;

			//get the entry children
			Repeater rptEntries = (Repeater)e.Item.FindControl("rptEntries");
			rptEntries.DataSource = r.ResultEntries;
			rptEntries.DataBind();
		}

		protected string FixDescription(string description) {
			string url = "https://" + Request.Url.Host + "/";
			string newString = description.Replace("href=\"/", "href=\"" + url).Replace("src=\"/", "src=\"" + url).Replace("src=\"~/", "src=\"" + url + "~/");

			return "<![CDATA[" + newString + "]]>";
		}
	}
}