using NUnit.Core;
using NUnit.Framework;
using Sitecore.TestStar.Core.Extensions;
using Sitecore.TestStar.Core.Providers;
using Sitecore.TestStar.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.SelfTests {
    [TestFixture, Category("Extension Tests")]
    public class ExtensionTests {

        #region SitecoreExtensions

        [Test]
        public void SCExtensions_ToDateFieldValue() {
            DateTime dt = new DateTime(2015, 1, 1, 1, 1, 1);
            string expected = "20150101T010101";

            string result = SitecoreExtensions.ToDateFieldValue(dt);

            Assert.AreEqual(result, expected);
        }

        #endregion SitecoreExtensions

        #region TestExtensions

        [Test]
        public void TestExtensions_SetProperty_TestMethod() {
            TestMethod method = TestUtility.GetTestSuite(UTAssemblyProvider.UTTestAssemblyName).GetMethodsByCategory(MockTests.Category).First();
            
            Assert.IsNotNull(method);

            string propKey = "propKey";
            bool propValue = true;

            method.SetProperty(propKey, propValue);
            bool result = (bool)method.Properties[propKey];

            Assert.IsTrue(result);
        }

        [Test]
        public void TestExtensions_SetProperty_Test() {
            TestSuite suite = TestUtility.GetTestSuite(UTAssemblyProvider.UTTestAssemblyName);

            Assert.IsNotNull(suite);

            string propKey = "propKey";
            bool propValue = true;

            suite.SetProperty(propKey, propValue);
            bool result = (bool)suite.Properties[propKey];

            Assert.IsTrue(result);
        }

        [Test]
        public void TestExtensions_GetProperty_TestMethod() {
            TestMethod method = TestUtility.GetTestSuite(UTAssemblyProvider.UTTestAssemblyName).GetMethodsByCategory(MockTests.Category).First();

            Assert.IsNotNull(method);

            string propKey = "propKey";
            bool propValue = true;

            method.Properties[propKey] = propValue;
            bool result = method.GetProperty<bool>(propKey);

            Assert.IsTrue(result);
        }

        [Test]
        public void TestExtensions_GetProperty_Test() {
            TestSuite suite = TestUtility.GetTestSuite(UTAssemblyProvider.UTTestAssemblyName);

            Assert.IsNotNull(suite);

            string propKey = "propKey";
            bool propValue = true;

            suite.Properties[propKey] = propValue;
            bool result = suite.GetProperty<bool>(propKey);

            Assert.IsTrue(result);
        }

        [Test]
        public void TestExtensions_GetMethodsByCategory() {
            IEnumerable<TestMethod> methods = TestUtility.GetTestSuite(UTAssemblyProvider.UTTestAssemblyName).GetMethodsByCategory(MockTests.Category);

            //make sure they are found
            Assert.IsTrue(methods.Any());

            //check that it found all 3
            Assert.AreEqual(methods.Count(), 3);
        }

        [Test]
        public void TestExtensions_GetAllCategories() {
            IEnumerable<string> cats = TestUtility.GetTestSuite(UTAssemblyProvider.UTTestAssemblyName).GetAllCategories();

            //make sure they're found
            Assert.IsTrue(cats.Any());

            //make sure the count is right
            Assert.AreEqual(cats.Count(), 5);
        }

        #endregion TestExtensions
    }
}
