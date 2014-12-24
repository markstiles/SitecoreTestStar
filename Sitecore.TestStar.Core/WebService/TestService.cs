﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services;
using NUnit.Core;
using Sitecore.Sites;
using Sitecore.TestStar.Core.Utility;
using Sitecore.TestStar.Core.Extensions;
using Sitecore.TestStar.Core.Managers;
using Sitecore.TestStar.Core.Providers;
using Sitecore.TestStar.Core.WebService;
using Sitecore.TestStar.Core.Entities;
using Cons = Sitecore.TestStar.Core.Utility.Constants;
using Sitecore.Data.Items;
using System.IO;

namespace Sitecore.TestStar.WebService {
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	[System.Web.Script.Services.ScriptService]
	public class TestService : System.Web.Services.WebService {

		[WebMethod]
		public List<JSONUnitTestResult> RunUnitTests(string AssemblyName, string Category) {
			CoreExtensions.Host.InitializeService();
			WebServiceUnitTestHandler wsuth = new WebServiceUnitTestHandler();
			UnitTestManager manager = new UnitTestManager(wsuth);

			//build dictionary with any methods with the selected categories
			Dictionary<string, TestMethod> sets = new Dictionary<string, TestMethod>();
			foreach (TestFixture tf in TestUtility.GetTestSuite(AssemblyName).GetFixtures()) {
				bool fixtHasCat = (tf.Categories().Any(b => b.Equals(Category)));
				foreach (TestMethod tm in tf.Tests) {
					//don't add twice and if fixture or the method has the selected category then add
					if (!sets.ContainsKey(tm.MethodName) && (fixtHasCat || tm.Categories().Any(b => b.Equals(Category))))
						sets.Add(tm.MethodName, tm);
				}
			}

			//run all tests found
			foreach (TestMethod method in sets.Values)
				manager.RunTest(method);

			return wsuth.ResultList;
		}

        [WebMethod]
        public List<JSONWebTestResult> RunWebTest(string EnvironmentID, string SiteID, string AssemblyName, string ClassName) {

            CoreExtensions.Host.InitializeService();
            WebServiceWebTestHandler wswth = new WebServiceWebTestHandler();
            WebTestManager manager = new WebTestManager(wswth);

            Item ei = TestStar.Core.Utility.Constants.MasterDB.GetItem(EnvironmentID);
            //if(ei == null)
            //    return error;
            TestEnvironment te = Factory.GetTestEnvironment(ei);

            Item si = TestStar.Core.Utility.Constants.MasterDB.GetItem(SiteID);
            //if(ei == null)
            //    return error;
            TestSite ts = Factory.GetTestSite(si);

            TestFixture tf = TestUtility.GetTestSuite(AssemblyName).GetFixtures().Where(a => a.ClassName.Equals(ClassName)).FirstOrDefault();
            //if (tf == null)
            //    return error;

            manager.RunTest(tf, te, ts);

            return wswth.ResultList;
        }

		[WebMethod]
		///@TestCalls is a list of strings in the format AssemblyName::Category
		public JSONGenScriptResult CreateUnitTestScript(string ScriptName, List<string> TestCalls) {
			if (string.IsNullOrEmpty(ScriptName)) 
				return new JSONGenScriptResult(false, "Script Generator: Need to provide a script name");
		
			if (!TestCalls.Any()) 
				return new JSONGenScriptResult(false, "Script Generator: Need to provide at least one test call");

			StringBuilder sb = new StringBuilder();
			sb.Append("@echo off").AppendLine();
			sb.Append(@"set TestLauncherPath=%0\..\..\bin\Sitecore.TestStar.TestLauncher.exe").AppendLine().AppendLine();
			sb.AppendLine("@echo on");

			//define exe, assembly, categories and name(blank)
			foreach (string s in TestCalls) {
				string[] arr = s.Split(new string[] { "::" }, StringSplitOptions.RemoveEmptyEntries);
				if (arr.Length < 2)
					continue;
				sb.AppendFormat("\"%TestLauncherPath%\" \"-u\" \"{0}\" \"{1}\" \"\"", arr[0], arr[1]).AppendLine();
			}
			sb.AppendLine().AppendLine("pause");

			//write file
			string filePath = string.Format(@"{0}sitecore modules\web\teststar\scripts\{1}.bat", Cons.ApplicationRoot, ScriptName);
			using (StreamWriter newData = new StreamWriter(filePath, false)) {
				newData.WriteLine(sb.ToString());
			}
			return new JSONGenScriptResult(true, string.Format("Script Generator: Successfully Created {0}", filePath));
		}

		[WebMethod]
		public JSONGenScriptResult CreateWebTestScript(string ScriptName, List<string> TestCalls, List<string> EnvironmentIDs, List<string> SiteIDs) {
			if (string.IsNullOrEmpty(ScriptName)) 
				return new JSONGenScriptResult(false, "Script Generator: Need to provide a script name");

			if (!TestCalls.Any())
				return new JSONGenScriptResult(false, "Script Generator: Need to provide at least one test call");

			StringBuilder sb = new StringBuilder();
			sb.Append("@echo off").AppendLine();
			sb.Append(@"set TestLauncherPath=%0\..\..\bin\Sitecore.TestStar.TestLauncher.exe").AppendLine().AppendLine();
			sb.AppendLine("@echo on");

			foreach (string s in TestCalls) {
				string[] arr = s.Split(new string[] { "::" }, StringSplitOptions.RemoveEmptyEntries);
				if (arr.Length < 2)
					continue;

				//define exe, assembly, test, environments, systems(blank) and sites
				sb.AppendFormat("\"%TestLauncherPath%\" \"-w\" \"{0}\" \"{1}\" \"{2}\" \"\" \"{3}\"", arr[0], arr[1], string.Join(",", EnvironmentIDs), string.Join(",", SiteIDs)).AppendLine();
			}

			sb.AppendLine().AppendLine("pause");

			//write file
			string filePath = string.Format(@"{0}sitecore modules\web\TestStar\scripts\{1}.bat", Cons.ApplicationRoot, ScriptName);
			using (StreamWriter newData = new StreamWriter(filePath, false)) {
				newData.WriteLine(sb.ToString());
			}
			return new JSONGenScriptResult(true, string.Format("Script Generator: Successfully Created {0}", filePath));
		}
	}
}
