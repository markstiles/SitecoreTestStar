using NUnit.Framework;
using Sitecore.TestStar.Core.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.SelfTests {
    [TestFixture, Category("Mock Tests")]
    public class MockTests {

        public static string Category = "Mock Tests";

        [Test]
        public void PassTest() {
            Assert.Pass();
        }
        
        [Test]
        public void FailTest() {
            Assert.Fail();
        }

        [Test]
        public void ErrorTest() {
            throw new Exception();
        }
    }
}
