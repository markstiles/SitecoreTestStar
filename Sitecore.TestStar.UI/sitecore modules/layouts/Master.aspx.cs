using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data.Items;
using Sitecore.Links;

namespace Sitecore.TestStar.Core.UI.layouts {
	public partial class Master : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {


			Item h = Sitecore.Context.Database.Items[Sitecore.Context.Site.StartPath];
			rptNav.DataSource = h.GetChildren().ToList(); ;
			rptNav.DataBind();
		}

		protected string GetLink(Item i) {
			string cssClass =  (Sitecore.Context.Item.ID.ToString().Equals(i.ID.ToString())) ? "class='active'" : string.Empty;
			UrlOptions u = new UrlOptions();
			u.LanguageEmbedding = LanguageEmbedding.Never;
			return string.Format("<a href='{0}'{1}>{2}</a>", LinkManager.GetItemUrl(i,u), cssClass, i.DisplayName);
		}
	}
}