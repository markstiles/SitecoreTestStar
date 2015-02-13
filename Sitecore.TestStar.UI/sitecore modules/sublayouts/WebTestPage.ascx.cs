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

            SCTextEntryProvider tProvider = new SCTextEntryProvider();
            IAssemblyProvider aProvider = (IAssemblyProvider)new SCAssemblyProvider(tProvider);
            rptSuites.DataSource = TestUtility.GetWebTestSuites(aProvider);
            rptSuites.DataBind();

            IEnvironmentProvider eProvider = (IEnvironmentProvider)new SCEnvironmentProvider(tProvider);
            rptEnvironments.DataSource = from ITestEnvironment te in eProvider.GetEnvironments()
                                         orderby te.Name
                                         select new ListItem(te.Name, te.ID);
            rptEnvironments.DataBind();

            ISiteProvider sProvider = (ISiteProvider)new SCSiteProvider(eProvider, tProvider);
            rptSites.DataSource = from ITestSite ts in sProvider.GetEnabledSites()
                                  orderby ts.SystemID, ts.Name
                                  select new ListItem(ts.Name, ts.ID);
            rptSites.DataBind();

            ISystemProvider sysProvider = (ISystemProvider)new SCSystemProvider(sProvider, tProvider);
            rptSystems.DataSource = from ITestSystem tsys in sysProvider.GetSystems()
                                    orderby tsys.Name
                                    select new ListItem(tsys.Name, tsys.ID);
            rptSystems.DataBind();
		}

        protected string GetSystemName(string siteID) {
            SCTextEntryProvider tProvider = new SCTextEntryProvider();
            IEnvironmentProvider eProvider = (IEnvironmentProvider)new SCEnvironmentProvider(tProvider);
            ISiteProvider sProvider = (ISiteProvider)new SCSiteProvider(eProvider, tProvider);
            ISystemProvider sysProvider = (ISystemProvider)new SCSystemProvider(sProvider, tProvider);
            ITestSite ts = sProvider.GetEnabledSites().Where(a => a.ID.Equals(siteID)).FirstOrDefault();
            return (ts == null || string.IsNullOrEmpty(ts.SystemID))
                ? string.Empty
                : sysProvider.GetSystems().Where(a => a.ID.Equals(ts.SystemID)).FirstOrDefault().Name;
        }

        protected string GetShortID(string scID) {
            return scID.Replace("{", string.Empty).Replace("}", string.Empty).Replace("-", string.Empty);
        }

        protected void rptSuites_ItemDataBound(object sender, RepeaterItemEventArgs e) {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;

            KeyValuePair<string, TestSuite> profile = (KeyValuePair<string, TestSuite>)e.Item.DataItem;

            Repeater rptCategories = (Repeater)e.Item.FindControl("rptCategories");
            rptCategories.DataSource = from string t in profile.Value.GetAllCategories().OrderBy(a => a) select new ListItem(t, profile.Key);
            rptCategories.DataBind();
        }

        protected string CondenseCatName(string className) {
            return className.Replace(".", string.Empty);
        }

		#endregion Events
	}
}