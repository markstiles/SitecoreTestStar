using System;
using System.Linq;
using System.Collections.Specialized;
using System.Data;
using System.Reflection;
using NUnit.Core;
using NUnit.Core.Filters;
using Sitecore.TestStar.Core.Utility;
using Sitecore.TestStar.Core.Extensions;
using Sitecore.TestStar.Core.Managers;
using System.Web.UI;
using Sitecore.TestStar.Core.Entities;
using System.Text;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.IO;
using Cons = Sitecore.TestStar.Core.Utility.Constants;
using Sitecore.TestStar.Core.Providers;

namespace Sitecore.TestStar.Core.UI.sublayouts {
	public partial class UnitTestPage : UserControl {

		#region Events

		protected void Page_Load(object sender, EventArgs e) {
			
			// Initialise NUnit
			CoreExtensions.Host.InitializeService();
			
			if (!IsPostBack) {
                rptSuites.DataSource = TestUtility.GetUnitTestSuites();
				rptSuites.DataBind();
			}
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

            //StringBuilder sb = new StringBuilder();
            //sb.Append("@echo off").AppendLine();
            //sb.AppendFormat(@"set TestLauncherPath=%0\..\..\bin\{0}.exe", Cons.DefaultTestLauncher).AppendLine().AppendLine();
            //sb.AppendLine("@echo on");

            //StringBuilder cats = new StringBuilder();
            ////foreach (ListItem li in cblCategories.Items.Cast<ListItem>().Where(a => a.Selected)) {
            ////	if (cats.Length > 0) 
            ////		cats.Append(",");
            ////	cats.Append(li.Value);
            ////}

            ////define exe, assembly, categories and name(blank)
            ////sb.AppendFormat("\"%TestLauncherPath%\" \"-u\" \"{0}\" \"{1}\" \"\"", Cons.DefaultUnitTestAssembly, cats.ToString());
            //sb.AppendLine().AppendLine("pause");

            ////write file
            //string filePath = string.Format(@"{0}/sitecore modules/web/TestStar/scripts/{1}.bat", Cons.ApplicationRoot, scriptName);
            //using (StreamWriter newData = new StreamWriter(filePath, false)) {
            //    newData.WriteLine(sb.ToString());
            //}
            //Log("Script Generator", string.Format("Successfully Created {0}", filePath));
		}

        protected void rptSuites_ItemDataBound(object sender, RepeaterItemEventArgs e) {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;

            KeyValuePair<string, TestSuite> profile = (KeyValuePair<string, TestSuite>)e.Item.DataItem;

            Repeater rptCategories = (Repeater)e.Item.FindControl("rptCategories");
            rptCategories.DataSource = profile.Value.GetAllCategories().OrderBy(a => a);
            rptCategories.DataBind();
        }

		#endregion Events
	}
}
