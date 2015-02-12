using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.SelfTests {
    [TestFixture, Category("Mock Tests")]
    public class MockTests {

        [Test]
        public void FailTest() {
            Assert.Fail();
        }

        [Test]
        public void PassTest() {
            Assert.Pass();
        }
    }
}
