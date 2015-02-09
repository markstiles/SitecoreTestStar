using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using NUnit.Core;
using NUnit.Core.Filters;
using NUnit.Framework;
using NUnit.Util;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Utility;
using Sitecore.TestStar.Core.Extensions;
using System.Text;
using Sitecore.TestStar.Core.Providers;
using Sitecore.TestStar.Core.Tests;
using Sitecore.TestStar.Core.Managers;
using System.IO;
using System.Net;
using Cons = Sitecore.TestStar.Core.Utility.Constants;
using Sitecore.TestStar.Core.Providers.Interfaces;
using Sitecore.TestStar.Core.Entities.Interfaces;

namespace Sitecore.TestStar.Core.UI.sublayouts {
	[RequiresSTA]
	public partial class WebTestPage : System.Web.UI.UserControl {

		#region Events
		
		/// <summary>
		/// Sets up the form
		/// </summary>
		protected void Page_Load(object sender, EventArgs e) {

			//setup for testing
			CoreExtensions.Host.InitializeService();

            IAssemblyProvider aProvider = (IAssemblyProvider)new SCAssemblyProvider();
            rptSuites.DataSource = TestUtility.GetWebTestSuites(aProvider);
            rptSuites.DataBind();

            IEnvironmentProvider eProvider = (IEnvironmentProvider)new SCEnvironmentProvider();
            rptEnvironments.DataSource = from ITestEnvironment te in eProvider.GetEnvironments()
                                         orderby te.Name
                                         select new ListItem(te.Name, te.ID);
            rptEnvironments.DataBind();

            ISystemProvider sysProvider = (ISystemProvider)new SCSystemProvider();
            rptSystems.DataSource = from ITestSystem tsys in sysProvider.GetSystems()
                                    orderby tsys.Name
                                    select new ListItem(tsys.Name, tsys.ID);
            rptSystems.DataBind();

            ISiteProvider sProvider = (ISiteProvider)new SCSiteProvider();
            rptSites.DataSource = from ITestSite ts in sProvider.GetEnabledSites(eProvider)
                                  orderby ts.SystemID, ts.Name
                                  select new ListItem(ts.Name, ts.ID);
            rptSites.DataBind();
		}

        protected string GetSystemName(string siteID) {
            IEnvironmentProvider eProvider = (IEnvironmentProvider)new SCEnvironmentProvider();
            ISystemProvider sysProvider = (ISystemProvider)new SCSystemProvider();
            ISiteProvider sProvider = (ISiteProvider)new SCSiteProvider();
            ITestSite ts = sProvider.GetEnabledSites(eProvider).Where(a => a.ID.Equals(siteID)).FirstOrDefault();
            return (ts == null || string.IsNullOrEmpty(ts.SystemID))
                ? string.Empty
                : sysProvider.GetSystems().Where(a => a.ID.Equals(ts.SystemID)).FirstOrDefault().Name;
        }

        protected string GetShortID(string scID) {
            return scID.Replace("{", string.Empty).Replace("}", string.Empty).Replace("-", string.Empty);
        }

        protected string CondenseClassName(string className) {
            return className.Replace(".", string.Empty);
        }

        protected void rptSuites_ItemDataBound(object sender, RepeaterItemEventArgs e) {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;

            KeyValuePair<string, TestSuite> profile = (KeyValuePair<string, TestSuite>)e.Item.DataItem;

            Repeater rptFixtures = (Repeater)e.Item.FindControl("rptFixtures");
            rptFixtures.DataSource = from TestFixture t in profile.Value.GetFixtures() select new ListItem(t.ClassName, profile.Key);
            rptFixtures.DataBind();
        }

		#endregion Events
	}
}