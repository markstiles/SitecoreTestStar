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

        [Test]
        public void UnitTestHandler_HandleTest() {

            UnitTestManager utManager = new UnitTestManager(new UTTextEntryProvider());

            IEnumerable<TestMethod> methods = TestUtility.GetTestSuite(UTAssemblyProvider.UTTestAssemblyName).GetMethodsByCategory(MockTests.Category);
                
			//run all tests found
            foreach (TestMethod m in methods)
                utManager.RunTest(m);

			//ensure you have one of each
            Assert.AreEqual(utManager.ResultList.Count, 3);
            Assert.IsTrue(utManager.ResultList.Where(a => a.Type.Equals("Success")).Any());
            Assert.IsTrue(utManager.ResultList.Where(a => a.Type.Equals("Failure")).Any());
            Assert.IsTrue(utManager.ResultList.Where(a => a.Type.Equals("Error")).Any());
            
            utManager.ResultList.Clear();
		}

        [Test]
        public void WebTestHandler_HandleTest() {

            WebTestManager wtManager = new WebTestManager(new UTTextEntryProvider());
        
            //get the environment
            IEnvironmentProvider eProvider = (IEnvironmentProvider)new UTEnvironmentProvider();
            ITestEnvironment te = eProvider.GetEnvironments().First();

            //get the site
            UTSiteProvider sProvider = new UTSiteProvider(eProvider);
            ITestSite ts = sProvider.GetSites().First();

            //get the test fixture
            IEnumerable<TestMethod> methods = TestUtility.GetTestSuite(UTAssemblyProvider.UTTestAssemblyName).GetMethodsByCategory(MockTests.Category);

            //run all tests found
            foreach (TestMethod m in methods)
                wtManager.RunTest(m, te, ts);

			//ensure you have one of each
			Assert.AreEqual(wtManager.ResultList.Count, 3);
            Assert.IsTrue(wtManager.ResultList.Where(a => a.Type.Equals("Success")).Any());
            Assert.IsTrue(wtManager.ResultList.Where(a => a.Type.Equals("Failure")).Any());
            Assert.IsTrue(wtManager.ResultList.Where(a => a.Type.Equals("Error")).Any());

            wtManager.ResultList.Clear();
        }
    }
}
