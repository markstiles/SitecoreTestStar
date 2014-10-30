using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.TestStar.Core.Utility;

namespace Sitecore.TestStar.Core.Entities {
	public class TestSite {
		
		#region Properties

		public int ID = -1;
		public string Name = string.Empty;
		public string Domain = string.Empty;
		public int SystemID = -1;
		public bool Disabled = false;

		public Dictionary<string, object> Properties;
		public IEnumerable<TestEnvironment> Environments;
		
		#endregion Properties

		/// <summary>
		/// Concatenates the Domain Prefix and the Domain
		/// </summary>
		/// <param name="env"></param>
		/// <returns></returns>
		public virtual string BaseURL(TestEnvironment env) {
			IEnumerable<TestEnvironment> envs = Environments.Where(e => e.ID.Equals(env.ID));
			if (envs == null || !envs.Any(e => e.ID.Equals(env.ID)))
				return string.Empty;
			TestEnvironment te = envs.First();
			return string.Format("{0}{1}", ((string.IsNullOrEmpty(te.DomainPrefix)) ? env.DomainPrefix : te.DomainPrefix), Domain);
		}
	}
}
