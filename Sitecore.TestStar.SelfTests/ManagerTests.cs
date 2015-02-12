using NUnit.Core;
using NUnit.Framework;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Entities.Interfaces;
using Sitecore.TestStar.Core.Extensions;
using Sitecore.TestStar.Core.Managers;
using Sitecore.TestStar.Core.Providers;
using Sitecore.TestStar.Core.Providers.Interfaces;
using Sitecore.TestStar.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.SelfTests {
    [TestFixture, Category("Manager Tests")]
    public class ManagerTests {

        [Test]
        public void UnitTestHandler_Null() {
            
            string AssemblyName = new UTAssemblyProvider().GetUnitTestAssemblies().First();
            string Category = "Mock Tests";

			//warm up nunit and configure the manager and handler
			CoreExtensions.Host.InitializeService();
			UnitTestManager manager = new UnitTestManager(new UTTextEntryProvider());

			//build dictionary with any methods with the selected categories
			Dictionary<string, TestMethod> sets = new Dictionary<string, TestMethod>();
			foreach (TestFixture tf in TestUtility.GetTestSuite(AssemblyName).GetFixtures()) {
				bool fixtHasCat = (tf.Categories().Any(b => b.Equals(Category)));
				foreach (TestMethod tm in tf.Tests) {
					if (!sets.ContainsKey(tm.MethodName) && (fixtHasCat || tm.Categories().Any(b => b.Equals(Category))))
						sets.Add(tm.MethodName, tm);
				}
			}

            //check that it found both
            Assert.AreEqual(sets.Count, 2);

			//run all tests found
			foreach (TestMethod method in sets.Values)
				manager.RunTest(method);

            Assert.AreEqual(manager.ResultList.Count, 2);
            
		}

        [Test]
        public void WebTestHandler_Null() {

            string AssemblyName = new UTAssemblyProvider().GetUnitTestAssemblies().First();
            string ClassName = "MockTests";

            //warm up nunit and configure the manager and handler
            CoreExtensions.Host.InitializeService();
            WebTestManager manager = new WebTestManager(new UTTextEntryProvider());

            //get the environment
            IEnvironmentProvider eProvider = (IEnvironmentProvider)new UTEnvironmentProvider();
            ITestEnvironment te = eProvider.GetEnvironments().First();

            //get the site
            UTSiteProvider sProvider = new UTSiteProvider();
            ITestSite ts = sProvider.GetSites(eProvider).First();

            //get the test fixture
            TestFixture tf = TestUtility.GetTestSuite(AssemblyName).GetFixtures().Where(a => a.ClassName.Equals(ClassName)).FirstOrDefault();
            Assert.IsNotNull(tf);

            manager.RunTest(tf, te, ts);
        }
    }
}
