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

namespace Sitecore.TestStar.WebService {
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	[System.Web.Script.Services.ScriptService]
	public class TestService : System.Web.Services.WebService {

		[WebMethod]
		public List<string> GetCategories(string TestSuiteName) {
			CoreExtensions.Host.InitializeService();
			TestSuite t = TestUtility.GetTestSuite(TestSuiteName);
			return t.GetAllCategories().OrderBy(a => a).ToList();
		}

		[WebMethod]
		public List<JSONTestResult> RunUnitTests(List<string> Categories) {
			CoreExtensions.Host.InitializeService();
			WebServiceUnitTestHandler wsuth = new WebServiceUnitTestHandler();
			UnitTestManager manager = new UnitTestManager(wsuth);

			//build dictionary with any methods with the selected categories
			Dictionary<string, TestMethod> sets = new Dictionary<string, TestMethod>();
			if (Categories.Any()) {
				foreach (string s in Categories) {
					foreach (TestFixture tf in TestUtility.GetFixtures()){
						bool fixtHasCat = (tf.Categories().Any(b => b.Equals(s)));
						foreach (TestMethod tm in tf.Tests) {
							//don't add twice and if fixture or the method has the selected category then add
							if (!sets.ContainsKey(tm.MethodName) && (fixtHasCat || tm.Categories().Any(b => b.Equals(s))))
								sets.Add(tm.MethodName, tm);
						}
					}
				}
			} else { // add all
				sets = TestUtility.GetSuites().SelectMany(a => a.Value.GetMethods()).ToDictionary(a => a.MethodName);
			}

			//run all tests found
			foreach (TestMethod method in sets.Values)
				manager.RunTest(method);

			return wsuth.ResultList;
		}
	}
}
