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
using Sitecore.TestStar.Core.Entities.Interfaces;

namespace Sitecore.TestStar.Core.Providers {
	public class SCSiteProvider : ISiteProvider {

		public IEnumerable<ITestSite> GetSites(IEnvironmentProvider eProvider) {
			Item folder = Cons.MasterDB.GetItem(Cons.SiteFolder);
			if (folder == null)
				throw new NullReferenceException(SCTextEntryProvider.Exceptions.Providers.EnvFoldNull);

            if (!folder.HasChildren)
                return Enumerable.Empty<ITestSite>();

			IEnumerable<ITestSite> sites = from Item i in folder.Axes.GetDescendants()
										  where i.TemplateID.ToString().Equals(Cons.SiteTemplate)
										  select FillTestSite(eProvider, i);
			return sites;
		}

        public IEnumerable<ITestSite> GetEnabledSites(IEnvironmentProvider eProvider) {
			return GetSites(eProvider).Where(a => !a.Disabled);
		}

        public ITestSite FillTestSite(IEnvironmentProvider eProvider, Item i) {
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

            List<ITestEnvironment> e = new List<ITestEnvironment>();
            DelimitedField df = i.Fields["Environments"];
            if (df != null) {
                foreach (string id in df.Items) {
                    Item env = Cons.MasterDB.GetItem(id);
                    if (env != null)
                        e.Add(eProvider.FillTestEnvironment(env));
                }
            }
            return (ITestSite)new TestSite(i.ID.ToString(), i.DisplayName, i.GetSafeFieldValue("Domain"), i.ParentID.ToString(), i.GetSafeFieldBool("Disabled"), p, e);
        }
	}
}
