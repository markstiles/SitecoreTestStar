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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.SelfTests {
    [TestFixture, Category("Manager Tests")]
    public class ManagerTests {

        private string AssemblyName;
        private string Category;
        private string ClassName;
        private UnitTestManager utManager;
        private WebTestManager wtManager;
            
        [SetUp]
        public void Setup() {
            AssemblyName = new UTAssemblyProvider().GetUnitTestAssemblies().First();
            Category = "Mock Tests";
            ClassName = "MockTests";
            
            //warm up nunit and configure the manager and handler
            CoreExtensions.Host.InitializeService();

            utManager = new UnitTestManager(new UTTextEntryProvider());
            wtManager = new WebTestManager(new UTTextEntryProvider());
        }

        [Test]
        public void UnitTestHandler_Null() {

            IEnumerable<TestMethod> methods = TestUtility.GetTestSuite(AssemblyName).GetMethodsByCategory(Category);
                
            //make sure they are found
            Assert.IsTrue(methods.Any());

            //check that it found all 3
            Assert.AreEqual(methods.Count(), 3);

			//run all tests found
            foreach (TestMethod m in methods)
                utManager.RunTest(m);

            Assert.AreEqual(utManager.ResultList.Count, 3);
            Assert.IsTrue(utManager.ResultList[0].Type.Equals("Failure"));
            Assert.IsTrue(utManager.ResultList[1].Type.Equals("Success"));
            Assert.IsTrue(utManager.ResultList[2].Type.Equals("Error"));
            
            wtManager.ResultList.Clear();
		}

        [Test]
        public void WebTestHandler_Null() {

            wtManager = new WebTestManager(new UTTextEntryProvider());

            //get the environment
            IEnvironmentProvider eProvider = (IEnvironmentProvider)new UTEnvironmentProvider();
            ITestEnvironment te = eProvider.GetEnvironments().First();

            //get the site
            UTSiteProvider sProvider = new UTSiteProvider(eProvider);
            ITestSite ts = sProvider.GetSites().First();

            //get the test fixture
            TestFixture tf = TestUtility.GetTestSuite(AssemblyName).GetFixtures().Where(a => a.ClassName.Equals(ClassName)).FirstOrDefault();
            Assert.IsNotNull(tf);

            wtManager.RunTest(tf, te, ts);

            wtManager.ResultList.Clear();
        }
    }
}
