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

		/// <summary>
		/// Generates a batch script from the configured test on the page
		/// </summary>
		protected void btnCreateScript_Click(object sender, EventArgs e) {
            //string scriptName = txtScriptName.Text;
            //if (string.IsNullOrEmpty(scriptName)) {
            //    LogError("Script Generator", "Need to provide a script name");
            //    return;
            //}

            //IEnumerable<TestEnvironment> envs = from ListItem li in cblEnv.Items.Cast<ListItem>() where li.Selected select Environments[li.Value];
            //IEnumerable<TestSite> sites = from ListItem li in cblSites.Items.Cast<ListItem>() where li.Selected select Sites[li.Value];
			
            //StringBuilder sb = new StringBuilder();
            //sb.Append("@echo off").AppendLine();
            //sb.AppendFormat(@"set TestLauncherPath=%0\..\..\bin\{0}.exe", Cons.DefaultTestLauncher).AppendLine().AppendLine();
            //sb.AppendLine("@echo on");
			
            //foreach (ListItem li in cblTests.Items.Cast<ListItem>().Where(a => a.Selected)) {
            //    TestFixture tf = Fixtures[li.Value];
            //    if(tf == null)
            //        continue;
            //    //define exe, assembly and test
            //    sb.AppendFormat("\"%TestLauncherPath%\" \"-w\" \"{0}\" \"{1}\"", Cons.DefaultWebTestAssembly, TestUtility.GetClassName(tf.ClassName));
            //    StringBuilder envStr = new StringBuilder();
            //    foreach (TestEnvironment te in envs) {
            //        if (envStr.Length > 0)
            //            envStr.Append(",");
            //        envStr.AppendFormat("{0}", te.ID);
            //    }
            //    sb.AppendFormat(" \"{0}\"", envStr.ToString());

            //    //leave systems blank and open sites
            //    sb.Append(" \"\"");
            //    StringBuilder siteStr = new StringBuilder();
            //    foreach (TestSite ts in sites) {
            //        if (siteStr.Length > 0)
            //            siteStr.Append(",");
            //        siteStr.AppendFormat("{0}", ts.ID);
            //    }
            //    //close sites
            //    sb.AppendFormat(" \"{0}\"", siteStr.ToString());
            //}

            //sb.AppendLine().AppendLine("pause");

            ////write file
            //string filePath = string.Format(@"{0}/sitecore modules/web/TestStar/scripts/{1}.bat", Cons.ApplicationRoot, scriptName);
            //using (StreamWriter newData = new StreamWriter(filePath, false)) {
            //    newData.WriteLine(sb.ToString());
            //}
            //Log("Script Generator", string.Format("Successfully Created {0}", filePath));
		}

		#endregion Events
	}
}