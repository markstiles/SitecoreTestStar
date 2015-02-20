using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NUnit.Core;
using Sitecore.TestStar.Core.Utility;
using Sitecore.TestStar.Core.Providers;
using Sitecore.TestStar.UI.Providers;

namespace Sitecore.TestStar.UI.sublayouts {
	public partial class HomePage : System.Web.UI.UserControl {
		protected void Page_Load(object sender, EventArgs e) {
			// Initialise NUnit
			CoreExtensions.Host.InitializeService();

            SCTextEntryProvider tProvider = new SCTextEntryProvider();
            IAssemblyProvider aProvider = (IAssemblyProvider)new SCAssemblyProvider(tProvider);

            Dictionary<string, TestSuite> ut = TestUtility.GetUnitTestSuites(aProvider);
			ltlUCount.Text = string.Format("You're tracking {0} unit test assemblies", ut.Count);
			rptUSuites.DataSource = ut;
			rptUSuites.DataBind();

            Dictionary<string, TestSuite> wt = TestUtility.GetWebTestSuites(aProvider);
			ltlWCount.Text = string.Format("You're tracking {0} web test assemblies", wt.Count);
			rptWSuites.DataSource = wt;
			rptWSuites.DataBind();
		}
	}
}