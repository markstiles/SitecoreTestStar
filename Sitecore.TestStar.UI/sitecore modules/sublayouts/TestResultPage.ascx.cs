using System;
using System.Linq;
using System.Collections.Specialized;
using System.Data;
using System.Reflection;
using System.Web.UI;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Extensions;
using System.Text;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.IO;
using Sitecore.TestStar.Core.Providers;
using Sitecore.Web;
using Sitecore.Links;
using Sitecore.Data.Items;
using Sitecore.TestStar.Core.Providers.Interfaces;
using Sitecore.TestStar.Core.Entities.Interfaces;

namespace Sitecore.TestStar.Core.UI.sublayouts {
	public partial class TestResultPage : UserControl {

		#region Events

		protected void Page_Load(object sender, EventArgs e) {
			
			//choose results 
            ITestResultProvider tProvider = (ITestResultProvider)new SCTestResultProvider();
            List<ITestResultList> results = tProvider.GetResults().ToList();
			
			int page = 1;
			int maxPosts = 10;
			int.TryParse(WebUtil.GetQueryString("page", "1"), out page);
			string pageUrl = LinkManager.GetItemUrl(Sitecore.Context.Item);

			//if getting most recent
			if (results.Count > maxPosts) {

				//set link text
				lnkNext.Text = SCTextEntryProvider.ResultList.NextBtn;
				lnkNext2.Text = SCTextEntryProvider.ResultList.NextBtn;
				lnkPrev.Text = SCTextEntryProvider.ResultList.PrevBtn;
				lnkPrev2.Text = SCTextEntryProvider.ResultList.PrevBtn;

				//build page links
				int totalPages = (int)Math.Ceiling(decimal.Divide(results.Count, maxPosts));

				//if there are more pages than the current page
				if (page < totalPages) {
					string nextUrl = pageUrl + "?page=" + (page + 1).ToString();
					lnkNext.NavigateUrl = nextUrl;
					lnkNext.Visible = true;
					lnkNext2.NavigateUrl = nextUrl;
					lnkNext2.Visible = true;
				}

				//if you're on a page higher than 1 set the prev
				if (page > 1) {
					string qstring = (page == 2) ? string.Empty : "?page=" + (page - 1).ToString();
					string prevUrl = pageUrl + qstring;
					lnkPrev.NavigateUrl = prevUrl;
					lnkPrev.Visible = true;
					lnkPrev2.NavigateUrl = prevUrl;
					lnkPrev2.Visible = true;
				}

				int startPos = (page - 1) * maxPosts;
				int postsLeft = results.Count - startPos;
				int howMany = (postsLeft < maxPosts) ? postsLeft : maxPosts;
				//trim list for that page
				results = results.GetRange(startPos, howMany).ToList();
			}

			if (results.Count() > 0) {
				rptResults.DataSource = results;
				rptResults.DataBind();
			} else {
				ltlMessage.Text = SCTextEntryProvider.ResultList.NoResults;
			}
		}

		protected void rptResults_ItemDataBound(object sender, RepeaterItemEventArgs e) {
			if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;

            ITestResultList r = (ITestResultList)e.Item.DataItem;
			
			if (stored == null) 
				stored = r.Date;

			string sStr = stored.ToString("yyyyMMdd");
			string rStr = r.Date.ToString("yyyyMMdd");

			//only show each date once
			PlaceHolder phDateHead = (PlaceHolder)e.Item.FindControl("phDateHead");
			if (!sStr.Equals(rStr)) {
				phDateHead.Visible = true;
				stored = r.Date;
			}

			//get the entry children
			Repeater rptEntries = (Repeater)e.Item.FindControl("rptEntries");
			rptEntries.DataSource = r.ResultEntries;
			rptEntries.DataBind();
		}

		#endregion Events

		protected DateTime stored = DateTime.Now.AddDays(1);

		
	}
}
