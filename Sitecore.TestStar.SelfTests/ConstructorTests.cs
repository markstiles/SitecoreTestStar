using NUnit.Framework;
using Sitecore.TestStar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.SelfTests {
    [TestFixture, Category("Constructor Tests")]
    public class ConstructorTests {
        
		[Test]
        public void GenScriptResultTest() {
			bool success = false;
            string msg = "msg";
            
            GenScriptResult gsr = new GenScriptResult(success, msg);
            
            Assert.AreEqual(gsr.Success, success);
            Assert.AreEqual(gsr.Message, msg);
		}
        
        [Test]
		public void TestEnvironmentTest() {
			string id = "id";
            string name = "name";
            string domainPrefix = "domainPrefix";
            string ipAddress = "ipAddress";
            
            TestEnvironment te = new TestEnvironment(id, name, domainPrefix, ipAddress);
            
            Assert.AreEqual(te.ID, id);
            Assert.AreEqual(te.Name, name);
            Assert.AreEqual(te.DomainPrefix, domainPrefix);
            Assert.AreEqual(te.IPAddress, ipAddress);
		}

		[Test]
		public void TestResultListTest() {
            string id = "id";
            string title = "title";
            DateTime dt = DateTime.Now;
            
            TestResultList trl = new TestResultList(id, title, dt);
            
            Assert.AreEqual(trl.ID, id);
            Assert.AreEqual(trl.Title, title);
            Assert.IsTrue(System.DateTime.Equals(trl.Date, dt));
		}

        [Test]
        public void TestSiteTest() {
            string id = "id";
            string name = "name";
            string domain = "domain";
            string systemID = "systemID";
            bool disabled = false;
            
            Dictionary<string,object> properties = new Dictionary<string, object>();
            string key = "first";
            string value = "second";
            properties.Add(key, value);
            
            string teid = "teid";
            string tename = "tename";
            string domainPrefix = "domainPrefix";
            string ipAddress = "ipAddress";
            TestEnvironment te = new TestEnvironment(teid, tename, domainPrefix, ipAddress);
            List<TestEnvironment> environments = new List<TestEnvironment>();
            environments.Add(te);
            
            TestSite ts = new TestSite(id, name, domain, systemID, disabled, properties, environments);
            
            Assert.AreEqual(ts.ID, id);
            Assert.AreEqual(ts.Name, name);
            Assert.AreEqual(ts.Domain, domain);
            Assert.AreEqual(ts.SystemID, systemID);
            Assert.AreEqual(ts.Disabled, disabled);
            Assert.AreEqual(ts.Properties.Count, properties.Count);
            Assert.IsTrue(ts.Properties.ContainsKey(key));
            Assert.AreEqual((string)ts.Properties[key], (string)properties[key]);
            Assert.AreEqual(ts.Environments.Count(), environments.Count);
            Assert.AreEqual(ts.Environments.ToList()[0].ID, environments[0].ID);
        }

        [Test]
        public void TestSystemTest() {
            string id = "id";
            string name = "name";
            
            TestSystem ts = new TestSystem(id, name);

            Assert.AreEqual(ts.ID, id);
            Assert.AreEqual(ts.Name, name);
        }

        [Test]
        public void UnitTestResultTest() {
            string id = "id";
            DateTime dt = DateTime.Now;
            string type = "type";
            string method = "method";
            string className = "className";
            string msg = "msg";

            UnitTestResult utr = new UnitTestResult(id, dt, type, method, className, msg);

            Assert.AreEqual(utr.ID, id);
            Assert.IsTrue(System.DateTime.Equals(utr.Date, dt));
            Assert.AreEqual(utr.Type, type);
            Assert.AreEqual(utr.Method, method);
            Assert.AreEqual(utr.ClassName, className);
            Assert.AreEqual(utr.Message, msg);
        }

        [Test]
        public void WebTestResultTest() {
            string id = "id";
            DateTime dt = DateTime.Now;
            string type = "type";
            string method = "method";
            string className = "className";
            string msg = "msg";
            string site = "site";
            string env = "env";
            string url = "url";
            string status = "status";

            WebTestResult wtr = new WebTestResult(id, dt, type, method, className, msg, site, env, url, status);

            Assert.AreEqual(wtr.ID, id);
            Assert.IsTrue(System.DateTime.Equals(wtr.Date, dt));
            Assert.AreEqual(wtr.Type, type);
            Assert.AreEqual(wtr.Method, method);
            Assert.AreEqual(wtr.ClassName, className);
            Assert.AreEqual(wtr.Message, msg);
            Assert.AreEqual(wtr.Site, site);
            Assert.AreEqual(wtr.Environment, env);
            Assert.AreEqual(wtr.RequestURL, url);
            Assert.AreEqual(wtr.ResponseStatus, status);
        }
    }
}
