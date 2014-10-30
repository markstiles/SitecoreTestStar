using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Utility;

namespace Sitecore.TestStar.Core.Providers {
	public class SiteProvider {

		public static IEnumerable<TestSite> GetSites() {
			IEnumerable<TestSite> sites = JsonSerializer.GetObject<List<TestSite>>(filePath);
			if (sites == null)
				throw new NullReferenceException("Sitecore.TestStar.Core.SystemProvider.GetSites: Check the file path specified exists and that it's not malformed json.");
			return sites;
		}

		public static IEnumerable<TestSite> GetEnabledSites() {
			return GetSites().Where(a => !a.Disabled);
		}
	}
}
