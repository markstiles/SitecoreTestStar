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
using Sitecore.Configuration;

namespace Sitecore.TestStar.Core.Providers {
	public class SCSiteProvider : ISiteProvider {

		public IEnumerable<ITestSite> GetSites(IEnvironmentProvider eProvider) {
            Item folder = Cons.MasterDB.GetItem(Settings.GetSetting("TestStar.SiteFolder"));
			if (folder == null)
				throw new NullReferenceException(SCTextEntryProvider.Exceptions.Providers.EnvFoldNull);

            if (!folder.HasChildren)
                return Enumerable.Empty<ITestSite>();
            
			IEnumerable<ITestSite> sites = from Item i in folder.Axes.GetDescendants()
										   where i.TemplateID.ToString().Equals(Settings.GetSetting("TestStar.SiteTemplate"))
                                           select GetTestSite(eProvider, i);
			return sites;
		}

        public IEnumerable<ITestSite> GetEnabledSites(IEnvironmentProvider eProvider) {
			return GetSites(eProvider).Where(a => !a.Disabled);
		}

        public ITestSite GetTestSite(IEnvironmentProvider eProvider, Item i) {
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

            List<string> envs = i.GetSafeFieldList("Environments");

            return GetTestSite(i.ID.ToString(), i.DisplayName, i.GetSafeFieldValue("Domain"), i.ParentID.ToString(), i.GetSafeFieldBool("Disabled"), p, eProvider.GetEnvironments().Where(a => envs.Contains(a.ID)));
        }

        public ITestSite GetTestSite(string id, string name, string domain, string systemID, bool disabled, Dictionary<string, object> properties, IEnumerable<ITestEnvironment> envs) {
            return (ITestSite)new DefaultTestSite(id, name, domain, systemID, disabled, properties, envs);
        }
	}
}
