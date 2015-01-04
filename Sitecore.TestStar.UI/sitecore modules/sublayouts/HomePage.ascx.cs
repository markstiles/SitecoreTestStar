using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NUnit.Core;
using Sitecore.TestStar.Core.Utility;

namespace Sitecore.TestStar.Core.UI.sublayouts {
	public partial class HomePage : System.Web.UI.UserControl {
		protected void Page_Load(object sender, EventArgs e) {
			// Initialise NUnit
			CoreExtensions.Host.InitializeService();

			Dictionary<string, TestSuite> ut = TestUtility.GetUnitTestSuites();
			ltlUCount.Text = string.Format("You're tracking {0} unit test assemblies", ut.Count);
			rptUSuites.DataSource = ut;
			rptUSuites.DataBind();

			Dictionary<string, TestSuite> wt = TestUtility.GetWebTestSuites();
			ltlWCount.Text = string.Format("You're tracking {0} web test assemblies", wt.Count);
			rptWSuites.DataSource = wt;
			rptWSuites.DataBind();
		}
	}
}