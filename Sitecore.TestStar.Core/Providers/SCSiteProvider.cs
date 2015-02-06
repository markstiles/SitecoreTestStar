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
using Sitecore.Data.Fields;
using Sitecore.TestStar.Core.Providers.Interfaces;

namespace Sitecore.TestStar.Core.Providers {
	public class SCSiteProvider : ISiteProvider {

		public IEnumerable<TestSite> GetSites(IEnvironmentProvider eProvider) {
			Item folder = Cons.MasterDB.GetItem(Cons.SiteFolder);
			if (folder == null)
				throw new NullReferenceException(SCTextEntryProvider.Exceptions.Providers.EnvFoldNull);

            if (!folder.HasChildren)
                return Enumerable.Empty<TestSite>();

			IEnumerable<TestSite> sites = from Item i in folder.Axes.GetDescendants()
										  where i.TemplateID.ToString().Equals(Cons.SiteTemplate)
										  select FillTestSite(eProvider, i);
			return sites;
		}

        public IEnumerable<TestSite> GetEnabledSites(IEnvironmentProvider eProvider) {
			return GetSites(eProvider).Where(a => !a.Disabled);
		}

        public TestSite FillTestSite(IEnvironmentProvider eProvider, Item i) {
            Dictionary<string, object> p = new Dictionary<string, object>();
            if (i.HasChildren) {
                foreach (Item c in i.GetChildren()) {
                    string s = c.GetSafeFieldValue("Value");
                    string l = s.ToLower();
                    if (l.Contains("true") || l.Contains("false"))
                        p.Add(c.DisplayName, bool.Parse(l));
                    else
                        p.Add(c.DisplayName, s);
                }
            }

            List<TestEnvironment> e = new List<TestEnvironment>();
            DelimitedField df = i.Fields["Environments"];
            if (df != null) {
                foreach (string id in df.Items) {
                    Item env = Cons.MasterDB.GetItem(id);
                    if (env != null)
                        e.Add(eProvider.FillTestEnvironment(env));
                }
            }
            return new TestSite(i.ID.ToString(), i.DisplayName, i.GetSafeFieldValue("Domain"), i.ParentID.ToString(), i.GetSafeFieldBool("Disabled"), p, e);
        }
	}
}
