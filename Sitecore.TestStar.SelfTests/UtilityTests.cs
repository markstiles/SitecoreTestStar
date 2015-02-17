using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Sitecore.TestStar.Core.Utility;
using Sitecore.TestStar.Core.Providers;
using NUnit.Core;

namespace Sitecore.TestStar.SelfTests {
	[TestFixture, Category("Utility Tests")]
    public class UtilityTests {

        #region SitecoreUtility

        [Test]
        public void SCUtil_GetItemName() {
            string invalidName = "[new]";
            string validName = "new";
            
            string result = SitecoreUtility.GetItemName(invalidName);
            Assert.IsTrue(result.Equals(validName));
        }

        [Test]
        public void SCUtil_MakeHref() {
            string a = "http://sample.domain";

            string result = SitecoreUtility.MakeHref(a);
            Assert.IsTrue(result.Contains(a));
        }

        [Test]
        public void SCUtil_GetKeyStr() {
            string s = "someValue";

            string result = SitecoreUtility.GetKeyStr(s);
            Assert.IsTrue(result.Contains(s));
        }

        #endregion SitecoreUtility

        #region TestUtility

        [Test]
        public void TestUtil_GetUnitTestSuites() {
            UTAssemblyProvider aProvider = new UTAssemblyProvider();
            
            Dictionary<string, TestSuite> result = TestUtility.GetUnitTestSuites(aProvider);
            
            //check that you got one
            Assert.IsTrue(result.Count.Equals(1));
            
            //make sure it's the right one
            Assert.IsTrue(result.First().Key.Equals(UTAssemblyProvider.UTTestAssemblyName));
        }

        [Test]
        public void TestUtil_GetWebTestSuites() {
            UTAssemblyProvider aProvider = new UTAssemblyProvider();
            
            Dictionary<string, TestSuite> result = TestUtility.GetWebTestSuites(aProvider);

            //check that you got one
            Assert.IsTrue(result.Count.Equals(1));

            //make sure it's the right one
            Assert.IsTrue(result.First().Key.Equals(UTAssemblyProvider.UTTestAssemblyName));
        }

        [Test]
        public void TestUtil_GetTestSuite() {
            TestSuite result = TestUtility.GetTestSuite(UTAssemblyProvider.UTTestAssemblyName);

            Assert.IsTrue(result != null);
        }

        [Test]
        public void TestUtil_() {
            string fullPath = "Some.Test.LibraryPath";
            
            string result = TestUtility.GetClassName(fullPath);
            
            Assert.IsTrue(result.Equals("LibraryPath"));
        }

        #endregion TestUtility
    }
}
