using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Extensions;
using Sitecore.TestStar.Core.Utility;
using Cons = Sitecore.TestStar.Core.Utility.Constants;

namespace Sitecore.TestStar.Core.Providers {
	public class SiteProvider {

		public static IEnumerable<TestSite> GetSites() {
			Item folder = Cons.MasterDB.GetItem(Cons.EnvironmentFolder);
			if (folder == null)
				throw new NullReferenceException(Cons.Exceptions.EnvFoldNull);

            if (!folder.HasChildren)
                return Enumerable.Empty<TestSite>();

			IEnumerable<TestSite> sites = from Item i in folder.GetChildren()
										  select Factory.GetTestSite(i);
			return sites;
		}

		public static IEnumerable<TestSite> GetEnabledSites() {
			return GetSites().Where(a => !a.Disabled);
		}
	}
}
