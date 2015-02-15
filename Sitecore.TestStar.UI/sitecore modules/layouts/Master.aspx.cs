using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data.Items;
using Sitecore.Links;

namespace Sitecore.TestStar.UI.layouts {
	public partial class Master : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {

            ltlPageTitle.Text = Sitecore.Context.Item["Page Title"];

			Item h = Sitecore.Context.Database.Items[Sitecore.Context.Site.StartPath];
			rptNav.DataSource = h.GetChildren().ToList(); ;
			rptNav.DataBind();
		}

		protected string GetLink(Item i) {
			string cssClass =  (Sitecore.Context.Item.ID.ToString().Equals(i.ID.ToString())) ? "class='active'" : string.Empty;
			UrlOptions u = new UrlOptions();
			u.LanguageEmbedding = LanguageEmbedding.Never;
			string navTitle = i["Nav Title"];
			string navText = (string.IsNullOrEmpty(navTitle)) ? i.DisplayName : navTitle;
			string firstLetter = navText.ToArray().First().ToString();
			return string.Format("<a title='{3}' href='{0}'{1}><div>{2}</div><span>{3}</span></a>", LinkManager.GetItemUrl(i, u), cssClass, firstLetter, navText);
		}
	}
}