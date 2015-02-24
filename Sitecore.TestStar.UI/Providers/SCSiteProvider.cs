using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Extensions;
using Sitecore.TestStar.Core.Utility;
using Sitecore.TestStar.UI.Extensions;
using Sitecore.Data.Fields;
using Sitecore.TestStar.Core.Providers;
using Sitecore.TestStar.Core.Entities.Interfaces;
using Sitecore.Configuration;
using Sitecore.TestStar.UI.Utility;

namespace Sitecore.TestStar.UI.Providers {
	public class SCSiteProvider : ISiteProvider {
        
        private ITextEntryProvider TextProvider;
        private IEnvironmentProvider EnvProvider; 

        public SCSiteProvider(IEnvironmentProvider e, ITextEntryProvider t) {
            if (e == null)
                throw new NullReferenceException();
            EnvProvider = e; 
            if (t == null)
                throw new NullReferenceException();
            TextProvider = t;
        }

        public IEnumerable<ITestSite> GetSites() {
            Item folder = SitecoreUtility.MasterDB.GetItem(Settings.GetSetting("TestStar.SiteFolder"));
			if (folder == null)
				throw new NullReferenceException(TextProviderPaths.Exceptions.Providers.EnvFoldNull(TextProvider));

            if (!folder.HasChildren)
                return Enumerable.Empty<ITestSite>();
            
			IEnumerable<ITestSite> sites = from Item i in folder.Axes.GetDescendants()
										   where i.TemplateID.ToString().Equals(Settings.GetSetting("TestStar.SiteTemplate"))
                                           select GetTestSite(i);
			return sites;
		}

        public IEnumerable<ITestSite> GetEnabledSites() {
			return GetSites().Where(a => !a.Disabled);
		}

        public ITestSite GetTestSite(Item i) {

            //get the properties
            Dictionary<string, string> props = i.Axes.GetDescendants()
                .Where(p => p.TemplateID.ToString().Equals(Settings.GetSetting("TestStar.PropertyTemplate")))
                .Distinct()
                .ToDictionary(p => p.DisplayName, p => p.GetSafeFieldValue("Value"));

            List<string> envs = i.GetSafeFieldList("Environments");
            IEnumerable<ITestEnvironment> siteEnvs = EnvProvider.GetEnvironments().Where(a => envs.Contains(a.ID));
            
            //handle environment override values
            IEnumerable<Item> envOverrides = from Item s in i.Axes.GetDescendants()
                                             where s.TemplateID.ToString().Equals(Settings.GetSetting("TestStar.EnvironmentOverrideTemplate")) 
                                             select s;
            if(envOverrides.Any()){
                Dictionary<string, ITestEnvironment> de = siteEnvs.ToDictionary(a => a.ID);
                foreach(Item eo in envOverrides){
                    string eID = eo.GetSafeFieldValue("TestEnvironment");
                    if(de.ContainsKey(eID)) {
                        string dPrefix = eo.GetSafeFieldValue("DomainPrefix");
                        string ip = eo.GetSafeFieldValue("IPAddress");
                        
                        if(!string.IsNullOrEmpty(dPrefix))
                            de[eID].DomainPrefix = dPrefix;
                        if(!string.IsNullOrEmpty(ip))
                            de[eID].IPAddress = ip;
                    }
                }
                siteEnvs = de.Values;
            }

            return GetTestSite(i.ID.ToString(), i.DisplayName, i.GetSafeFieldValue("Domain"), i.ParentID.ToString(), i.GetSafeFieldBool("Disabled"), props, siteEnvs);
        }

        public ITestSite GetTestSite(string id, string name, string domain, string systemID, bool disabled, Dictionary<string, string> properties, IEnumerable<ITestEnvironment> envs) {
            return (ITestSite)new DefaultTestSite(id, name, domain, systemID, disabled, properties, envs);
        }
	}
}
