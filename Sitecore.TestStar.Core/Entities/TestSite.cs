using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.TestStar.Core.Utility;
using Sitecore.TestStar.Core.Entities.Interfaces;

namespace Sitecore.TestStar.Core.Entities {
	public class TestSite {

		#region ITestSite

        protected string _ID;
		public string ID { get { return _ID; } set { _ID = value; } }
        protected string _Name;
        public string Name { get { return _Name; } set { _Name = value; } }
        protected string _Domain;
        public string Domain { get { return _Domain; } set { _Domain = value; } }
        protected string _SystemID;
        public string SystemID { get { return _SystemID; } set { _SystemID = value; } }
        protected bool _Disabled;
        public bool Disabled { get { return _Disabled; } set { _Disabled = value; } }
        protected string _LanguageCode;
        public string LanguageCode { get { return _LanguageCode; } set { _LanguageCode = value; } }
        protected string _SiteNodeID;
        public string SiteNodeID { get { return _SiteNodeID; } set { _SiteNodeID = value; } }

        protected Dictionary<string, object> _Properties;
        public Dictionary<string, object> Properties;
        protected IEnumerable<ITestEnvironment> _Environments;
        public IEnumerable<ITestEnvironment> Environments;

        #endregion ITestSite

        public TestSite(string id, string name, string domain, string systemID, bool disabled, Dictionary<string, object> properties, IEnumerable<ITestEnvironment> environments) {
			ID = id;
			Name = name;
			Domain = domain;
			SystemID = systemID;
			Disabled = disabled;
			Properties = properties;
			Environments = environments;
		}

        /// <summary>
		/// Concatenates the Domain Prefix and the Domain
		/// </summary>
		/// <param name="env"></param>
		/// <returns></returns>
		public virtual string BaseURL(ITestEnvironment env) {
			IEnumerable<ITestEnvironment> envs = Environments.Where(e => e.ID.Equals(env.ID));
			if (envs == null || !envs.Any(e => e.ID.Equals(env.ID)))
				return string.Empty;
			ITestEnvironment te = envs.First();
			string langChunk = (string.IsNullOrEmpty(LanguageCode))
				? string.Empty
				: string.Format("/{0}", LanguageCode);
			return string.Format("{0}{1}{2}", ((string.IsNullOrEmpty(te.DomainPrefix)) ? env.DomainPrefix : te.DomainPrefix), Domain, langChunk);
		}
	}
}
