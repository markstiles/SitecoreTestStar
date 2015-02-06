﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NUnit.Core;
using Sitecore.TestStar.Core.Utility;
using Sitecore.TestStar.Core.Providers.Interfaces;
using Sitecore.TestStar.Core.Providers;

namespace Sitecore.TestStar.Core.UI.sublayouts {
	public partial class HomePage : System.Web.UI.UserControl {
		protected void Page_Load(object sender, EventArgs e) {
			// Initialise NUnit
			CoreExtensions.Host.InitializeService();

            IAssemblyProvider aProvider = (IAssemblyProvider)new SCAssemblyProvider();

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