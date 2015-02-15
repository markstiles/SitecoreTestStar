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
        
		[Test]
        public void Test() {
            //TestExtensions.SetProperty(); //x2 one for TestMethod one for Test
            //TestExtensions.GetProperty(); //x2 one for TestMethod one for ITest
            //TestExtensions.GetMethod();
            //TestExtensions.GetMethods();
			//TestExtensions.GetMethodsByCategory(this TestSuite suite, string category)
            //TestExtensions.GetFixtures();
            //TestExtensions.GetAllCategories();
            //TestExtensions.Categories();

			//SitecoreExtensions.GetItemByID
			//SitecoreExtensions.GetSafeFieldValue
			//SitecoreExtensions.GetSafeFieldList
			//SitecoreExtensions.GetSafeFieldBool
			//SitecoreExtensions.GetSafeDateFieldValue
			//SitecoreExtensions.ToDateFieldValue
		}
    }
}
