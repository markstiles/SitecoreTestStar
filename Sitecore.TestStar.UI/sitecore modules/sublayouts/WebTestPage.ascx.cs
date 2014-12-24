﻿using System;
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
			
			rptSuites.DataSource = TestUtility.GetWebTestSuites();
            rptSuites.DataBind();
				
            rptEnvironments.DataSource = from TestEnvironment te in EnvironmentProvider.GetEnvironments()
                                         orderby te.Name
                                         select new ListItem(te.Name, te.ID);
            rptEnvironments.DataBind();

            rptSystems.DataSource = from TestSystem tsys in SystemProvider.GetSystems()
                                    orderby tsys.Name
                                    select new ListItem(tsys.Name, tsys.ID);
            rptSystems.DataBind();    

            rptSites.DataSource = from TestSite ts in SiteProvider.GetEnabledSites()
                                  orderby ts.SystemID, ts.Name
                                  select new ListItem(ts.Name, ts.ID);
            rptSites.DataBind();
		}

        protected string GetSystemName(string siteID) {
            TestSite ts = SiteProvider.GetEnabledSites().Where(a => a.ID.Equals(siteID)).FirstOrDefault();
            return (ts == null || string.IsNullOrEmpty(ts.SystemID))
                ? string.Empty
                : SystemProvider.GetSystems().Where(a => a.ID.Equals(ts.SystemID)).FirstOrDefault().Name;
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