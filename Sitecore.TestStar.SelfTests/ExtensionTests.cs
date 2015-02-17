using NUnit.Framework;
using Sitecore.TestStar.Core.Extensions;
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
        public void SCExtensions_() {
            //SitecoreExtensions.GetItemByID
        }

        [Test]
        public void SCExtensions_() {
            //SitecoreExtensions.GetSafeFieldValue
        }
        
        [Test]
        public void SCExtensions_() {
            //SitecoreExtensions.GetSafeFieldList
        }
        
        [Test]
        public void SCExtensions_() {
            //SitecoreExtensions.GetSafeFieldBool
        }
        
        [Test]
        public void SCExtensions_() {
            //SitecoreExtensions.GetSafeDateFieldValue
        }
        
        [Test]
        public void SCExtensions_() {
            //SitecoreExtensions.ToDateFieldValue
        }

        #endregion SitecoreExtensions

        #region TestExtensions

        [Test]
        public void TestExtensions_() {
            //TestExtensions.SetProperty(); //x2 one for TestMethod one for Test
        }

        [Test]
        public void TestExtensions_() {

        }

        [Test]
        public void TestExtensions_() {
            //TestExtensions.GetProperty(); //x2 one for TestMethod one for ITest
        }

        [Test]
        public void TestExtensions_() {
            //TestExtensions.GetMethod();
        }

        [Test]
        public void TestExtensions_() {
            //TestExtensions.GetMethods();
        }

        [Test]
        public void TestExtensions_() {
            //TestExtensions.GetMethodsByCategory(this TestSuite suite, string category)
        }

        [Test]
        public void TestExtensions_() {
            //TestExtensions.GetFixtures();
        }

        [Test]
        public void TestExtensions_() {
            //TestExtensions.GetAllCategories();
        }

        [Test]
        public void TestExtensions_() {
            //TestExtensions.Categories();
        }

        #endregion TestExtensions
    }
}
