using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Extensions;

namespace Sitecore.TestStar.Core.Utility {
	public class Factory {

		public static TestSite GetTestSite(Item i){
			Dictionary<string, object> p = new Dictionary<string, object>();
			if(i.HasChildren) {
				foreach(Item c in i.GetChildren()){
					string s = c.GetSafeFieldValue("Value");
					string l = s.ToLower();
					if(l.Contains("true") || l.Contains("false"))
						p.Add(c.DisplayName, bool.Parse(l));
					else 
						p.Add(c.DisplayName, s);
				}
			}

			List<TestEnvironment> e = new List<TestEnvironment>();
			DelimitedField df = i.Fields["Environments"];
			if(df != null){
				foreach(string id in df.Items){
					Item env = Constants.MasterDB.GetItem(id);
					if (env != null)
						e.Add(GetTestEnvironment(env));
				}
			}
			return new TestSite(i.ID.ToString(), i.DisplayName, i.GetSafeFieldValue("Domain"), i.ParentID.ToString(), i.GetSafeFieldBool("Disabled"), p, e);
		}

		public static TestEnvironment GetTestEnvironment(Item i){
			return new TestEnvironment(i.ID.ToString(), i.DisplayName, i.GetSafeFieldValue("DomainPrefix"), i.GetSafeFieldValue("IPAddress"));
		}

		public static TestSystem GetTestSystem(Item i) {
			return new TestSystem(i.ID.ToString(), i.DisplayName);
		}

		public static TestResultList GetTestResult(Item i) {
			return new TestResultList(i.ID.ToString(), i.DisplayName, i.GetSafeDateFieldValue("Date"));
		}

		public static string GetTestAssembly(Item i) {
			return i.GetSafeFieldValue("AssemblyName");
		}
	}
}
