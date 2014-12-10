using System;
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
		public List<JSONUnitTestResult> RunUnitTests(List<string> Categories) {
			CoreExtensions.Host.InitializeService();
			WebServiceUnitTestHandler wsuth = new WebServiceUnitTestHandler();
			UnitTestManager manager = new UnitTestManager(wsuth);

			//build dictionary with any methods with the selected categories
			Dictionary<string, TestMethod> sets = new Dictionary<string, TestMethod>();
			if (Categories.Any()) {
				foreach (string s in Categories) {
					foreach (TestFixture tf in TestUtility.GetUnitTestFixtures()){
						bool fixtHasCat = (tf.Categories().Any(b => b.Equals(s)));
						foreach (TestMethod tm in tf.Tests) {
							//don't add twice and if fixture or the method has the selected category then add
							if (!sets.ContainsKey(tm.MethodName) && (fixtHasCat || tm.Categories().Any(b => b.Equals(s))))
								sets.Add(tm.MethodName, tm);
						}
					}
				}
			} else { // add all
                sets = TestUtility.GetUnitTestSuites().SelectMany(a => a.Value.GetMethods()).ToDictionary(a => a.MethodName);
			}

			//run all tests found
			foreach (TestMethod method in sets.Values)
				manager.RunTest(method);

			return wsuth.ResultList;
		}

        //[WebMethod]
        //public List<JSONWebTestResult> RunWebTests(string env, string site, string test) {
        
            //WebTestManager manager = new WebTestManager(this);
			
            //IEnumerable<TestEnvironment> envs = from ListItem li in cblEnv.Items.Cast<ListItem>() where li.Selected select Environments[li.Value];
            //IEnumerable<TestSite> sites = from ListItem li in cblSites.Items.Cast<ListItem>() where li.Selected select Sites[li.Value];
	
            //foreach (ListItem li in cblTests.Items.Cast<ListItem>().Where(a => a.Selected)) {
            //    TestFixture tf = Fixtures[li.Value];
            //    manager.RunTest(tf, envs, sites);
            //}
		//}
	}
}
