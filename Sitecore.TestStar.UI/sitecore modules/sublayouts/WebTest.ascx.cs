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
using Sitecore.TestStar.WebTests.Tests;
using Sitecore.TestStar.Core.Utility;
using Sitecore.TestStar.Core.Extensions;
using System.Text;
using Sitecore.TestStar.Core.Providers;
using Sitecore.TestStar.Core.Tests;
using Sitecore.TestStar.Core.Managers;
using System.IO;
using System.Net;

namespace Sitecore.TestStar.UI.sublayouts {
	[RequiresSTA]
	public partial class WebTest : System.Web.UI.UserControl, IWebTestHandler {

		#region Properties

		private Dictionary<string, TestMethod> _Methods;
		protected Dictionary<string, TestMethod> Methods {
			get {
				if(_Methods == null)
					_Methods = new Dictionary<string, TestMethod>();
				return _Methods;
			}
			set {
				_Methods = value;
			}
		}

		private Dictionary<string, TestFixture> _Fixtures;
		protected Dictionary<string, TestFixture> Fixtures {
			get {
				if (_Fixtures == null)
					_Fixtures = new Dictionary<string, TestFixture>();
				return _Fixtures;
			}
			set {
				_Fixtures = value;
			}
		}

		private Dictionary<int, TestEnvironment> _Environments;
		protected Dictionary<int, TestEnvironment> Environments {
			get {
				if (_Environments == null)
					_Environments = new Dictionary<int, TestEnvironment>();
				return _Environments;
			}
			set {
				_Environments = value;
			}
		}

		private Dictionary<int, TestSystem> _Systems;
		protected Dictionary<int, TestSystem> Systems {
			get {
				if (_Systems == null)
					_Systems = new Dictionary<int, TestSystem>();
				return _Systems;
			}
			set {
				_Systems = value;
			}
		}

		private Dictionary<int, TestSite> _Sites;
		protected Dictionary<int, TestSite> Sites {
			get {
				if (_Sites == null)
					_Sites = new Dictionary<int, TestSite>();
				return _Sites;
			}
			set {
				_Sites = value;
			}
		}
		
		#endregion Properties

		#region Events
		
		/// <summary>
		/// Sets up the form
		/// </summary>
		protected void Page_Load(object sender, EventArgs e) {

			//setup for testing
			CoreExtensions.Host.InitializeService();
			//get the test suite
			TestSuite suite = TestUtility.GetTestSuite(Constants.DefaultWebTestAssembly);
			
			//get dictionaries for forms and querying
			foreach (TestMethod tm in suite.GetMethods())
				Methods.Add(tm.TestName.FullName, tm);
			foreach (TestFixture tf in suite.GetFixtures())
				Fixtures.Add(tf.ClassName, tf);
			foreach (TestEnvironment t in EnvironmentProvider.GetEnvironments().OrderBy(a => a.Name)) 
				Environments.Add(t.ID, t);
			foreach (TestSystem tsys in SystemProvider.GetSystems().OrderBy(a => a.Name))
				Systems.Add(tsys.ID, tsys);
			foreach (TestSite ts in SiteProvider.GetEnabledSites().OrderBy(a => a.SystemID).ThenBy(a => a.Name)) {
				try {
					Sites.Add(ts.ID, ts);
				} catch (ArgumentException aex) {
					throw new ArgumentException(string.Format("This key has already been added: {0}-{1}", ts.SystemID, ts.Name));
				}
			}

			ltlResults.Text = string.Empty; //reset output
			ltlError.Text = string.Empty;
			ltlLog.Text = string.Empty;
			
			if (!IsPostBack) { //setup form
				foreach (KeyValuePair<string, TestFixture> kvp in Fixtures) {
					ListItem li = new ListItem(TestUtility.GetClassName(kvp.Value.ClassName), kvp.Value.TestName.FullName);
					cblTests.Items.Add(li);
				}
				foreach (KeyValuePair<int, TestEnvironment> ekvp in Environments) {
					ListItem li = new ListItem(ekvp.Value.Name, ekvp.Key.ToString());
					cblEnv.Items.Add(li);
				}
				foreach (KeyValuePair<int,TestSystem> sykvp in Systems) {
					ListItem li = new ListItem(sykvp.Value.Name, sykvp.Value.Name);
					cblSystems.Items.Add(li);
				}
				foreach (KeyValuePair<int, TestSite> skvp in Sites) {
					ListItem li = new ListItem(string.Format("{1}<span class='systemName'>{0}</span>", Systems[skvp.Value.SystemID].Name, skvp.Value.Name), skvp.Key.ToString());
					li.Attributes.Add("class", Systems[skvp.Value.SystemID].Name);
					cblSites.Items.Add(li);
				}
			} else {
				foreach (ListItem li in cblSites.Items) { //css classes get lost on postback
					li.Attributes.Add("class", Systems[Sites[int.Parse(li.Value)].SystemID].Name);
				}
			}
		}

		/// <summary>
		/// This method figures out what tests, environments and sites are to be tested and runs them separately in their own threads
		/// </summary>
		protected void btnSubmitTests_Click(object sender, EventArgs e) {

			WebTestManager manager = new WebTestManager(this);
			
			IEnumerable<TestEnvironment> envs = from ListItem li in cblEnv.Items.Cast<ListItem>() where li.Selected select Environments[int.Parse(li.Value)];
			IEnumerable<TestSite> sites = from ListItem li in cblSites.Items.Cast<ListItem>() where li.Selected select Sites[int.Parse(li.Value)];
	
			foreach (ListItem li in cblTests.Items.Cast<ListItem>().Where(a => a.Selected)) {
				TestFixture tf = Fixtures[li.Value];
				manager.RunTest(tf, envs, sites);
			}
		}

		/// <summary>
		/// Generates a batch script from the configured test on the page
		/// </summary>
		protected void btnCreateScript_Click(object sender, EventArgs e) {
			string scriptName = txtScriptName.Text;
			if (string.IsNullOrEmpty(scriptName)) {
				LogError("Script Generator", "Need to provide a script name");
				return;
			}

			IEnumerable<TestEnvironment> envs = from ListItem li in cblEnv.Items.Cast<ListItem>() where li.Selected select Environments[int.Parse(li.Value)];
			IEnumerable<TestSite> sites = from ListItem li in cblSites.Items.Cast<ListItem>() where li.Selected select Sites[int.Parse(li.Value)];
			
			StringBuilder sb = new StringBuilder();
			sb.Append("@echo off").AppendLine();
			sb.AppendFormat(@"set TestLauncherPath=%0\..\..\bin\{0}.exe", Constants.DefaultTestLauncher).AppendLine().AppendLine();
			sb.AppendLine("@echo on");
			
			foreach (ListItem li in cblTests.Items.Cast<ListItem>().Where(a => a.Selected)) {
				TestFixture tf = Fixtures[li.Value];
				if(tf == null)
					continue;
				//define exe, assembly and test
				sb.AppendFormat("\"%TestLauncherPath%\" \"-w\" \"{0}\" \"{1}\"", Constants.DefaultWebTestAssembly, TestUtility.GetClassName(tf.ClassName));
				StringBuilder envStr = new StringBuilder();
				foreach (TestEnvironment te in envs) {
					if (envStr.Length > 0)
						envStr.Append(",");
					envStr.AppendFormat("{0}", te.ID);
				}
				sb.AppendFormat(" \"{0}\"", envStr.ToString());

				//leave systems blank and open sites
				sb.Append(" \"\"");
				StringBuilder siteStr = new StringBuilder();
				foreach (TestSite ts in sites) {
					if (siteStr.Length > 0)
						siteStr.Append(",");
					siteStr.AppendFormat("{0}", ts.ID);
				}
				//close sites
				sb.AppendFormat(" \"{0}\"", siteStr.ToString());
			}

			sb.AppendLine().AppendLine("pause");

			//write file
			string filePath = string.Format(@"{0}/scripts/{1}.bat", Constants.ApplicationRoot, scriptName);
			using (StreamWriter newData = new StreamWriter(filePath, false)) {
				newData.WriteLine(sb.ToString());
			}
			Log("Script Generator", string.Format("Successfully Created {0}", filePath));
		}

		#endregion Events
		
		#region ITestHandler Events

		public void OnError(TestMethod tm, TestEnvironment te, TestSite ts, TestResult tr, string requestURL, HttpStatusCode responseStatus){
			Results(ts, te, tm, "Has Errors", tr.Message, "err");
		}

		public void OnFailure(TestMethod tm, TestEnvironment te, TestSite ts, TestResult tr, string requestURL, HttpStatusCode responseStatus) {
			Results(ts, te, tm, "Failed", tr.Message, "fail");
		}

		public void OnSuccess(TestMethod tm, TestEnvironment te, TestSite ts, TestResult tr, string requestURL, HttpStatusCode responseStatus) {
			Results(ts, te, tm, "Succeeded", string.Empty, "pass");
		}

		public void OnSkipped(TestMethod tm, TestEnvironment te, TestSite ts) {
			Results(ts, te, null, "Skipped", string.Format("{0} doesn't support the {1} environment", ts.Name, te.Name), "skip");
		}

		#endregion ITestHandler Events

		#region UI Messaging

		private bool ResultFlag = false;

		/// <summary>
		/// writes message to the results window
		/// </summary>
		protected void Results(TestSite ts, TestEnvironment te, TestMethod tm, string name, string value, string type) {
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("<div class='result {0} {1}'>", (ResultFlag) ? "even" : "odd" , type).AppendLine();
			sb.AppendFormat("<div class='rSite'>{0} - {1}</div>",ts.Name, te.Name).AppendLine();
			sb.Append("<div class='clearfix'></div>").AppendLine();
			if (tm != null) {
				sb.AppendFormat("<div class='rMethod'>{0} -</div>", TestUtility.GetClassName(tm.ClassName)).AppendLine();
			}
			sb.AppendFormat("<div class='rResult'>{0}{1}{2}</div>", name, (value.Length > 0) ? ": ": string.Empty, value).AppendLine();
			sb.Append("</div>").AppendLine();
			ltlResults.Text += sb.ToString();
			
			ResultFlag = !ResultFlag;
		}

		/// <summary>
		/// writes message to the log window
		/// </summary>
		protected void Log(string name, string value){
			ltlLog.Text += string.Format("{0}: {1}<br/>", name, value);
		}

		protected void LogError(string name, string value) {
			ltlError.Text += string.Format("{0}: {1}<br/>", name, value);
		}

		#endregion UI Messaging
	}
}