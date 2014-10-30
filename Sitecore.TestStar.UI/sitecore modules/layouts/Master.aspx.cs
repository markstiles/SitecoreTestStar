using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.TestStar.UI.layouts {
	public partial class Main : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {


			Dictionary<string, string> navLinks = new Dictionary<string,string>(){ 
				{"/unittest.aspx", "Unit Testing"},
				{"/webtest.aspx","Web Testing"},
				{"/manager.aspx","Web Testing Data Manager"}
			};
			rptNav.DataSource = navLinks;
			rptNav.DataBind();
		}

		protected string GetLink(KeyValuePair<string, string> link) {
			string cssClass = (HttpContext.Current.Request.Path.ToLower().Equals(link.Key)) ? "class='active'" : string.Empty;
			return string.Format("<a href='{0}'{1}>{2}</a>", link.Key, cssClass, link.Value);
		}
	}
}