using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.TestStar.Core.Entities;

namespace Sitecore.TestStar.WebTests.Entities {
	public class SitecoreSite : TestSite {

		public string LanguageCode {
			get {
				return GetProperty<string>("LanguageCode", string.Empty);
			}
		}
		public bool SearchPageExists {
			get {
				return GetProperty<bool>("SearchPageExists", true); 
			}
		}
		public string SearchPagePath {
			get {
				return GetProperty<string>("SearchPagePath", string.Empty);
			}
		}
		public string SiteNodeID {
			get {
				return GetProperty<string>("SiteNodeID", string.Empty);
			}
		}

		private T GetProperty<T>(string key, T defaultValue) {
			return (Properties.ContainsKey(key)) ? (T)Properties[key] : defaultValue;
		}

		/// <summary>
		/// Concatenates the Domain Prefix, Domain and LanguageCode if it exists
		/// </summary>
		/// <param name="env"></param>
		/// <returns></returns>
		public string SCBaseURL(TestEnvironment env) {
			return (string.IsNullOrEmpty(LanguageCode))
				? BaseURL(env)
				: string.Format("{0}/{1}", BaseURL(env), LanguageCode);
		}
	}
}
