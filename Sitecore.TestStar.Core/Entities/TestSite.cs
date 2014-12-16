using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.TestStar.Core.Utility;

namespace Sitecore.TestStar.Core.Entities {
	public class TestSite {

		#region Properties

		public string ID = string.Empty;
		public string Name = string.Empty;
		public string Domain = string.Empty;
		public string SystemID = string.Empty;
		public bool Disabled = false;
		public string LanguageCode = string.Empty;
		public string SiteNodeID = string.Empty;

		public Dictionary<string, object> Properties;
		public IEnumerable<TestEnvironment> Environments;

		public TestSite(string id, string name, string domain, string systemID, bool disabled, Dictionary<string, object> properties, IEnumerable<TestEnvironment> environments) {
			ID = id;
			Name = name;
			Domain = domain;
			SystemID = systemID;
			Disabled = disabled;
			Properties = properties;
			Environments = environments;
		}

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
			string langChunk = (string.IsNullOrEmpty(LanguageCode))
				? string.Empty
				: string.Format("/{0}", LanguageCode);
			return string.Format("{0}{1}{2}", ((string.IsNullOrEmpty(te.DomainPrefix)) ? env.DomainPrefix : te.DomainPrefix), Domain, langChunk);
		}
	}
}
