﻿using System;
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
	public class UTSiteProvider : ISiteProvider {

		public IEnumerable<ITestSite> GetSites(IEnvironmentProvider eProvider) {

            List<string> envs = new List<string>(){ "1" };

			IEnumerable<ITestSite> sites = new List<ITestSite>(){ 
                GetTestSite(
                    "1", 
                    "TestSite", 
                    "test.com", 
                    "1", 
                    false, 
                    new Dictionary<string, object>(), 
                    eProvider.GetEnvironments().Where(a => envs.Contains(a.ID))
                ) 
            };
			
            return sites;
		}

        public IEnumerable<ITestSite> GetEnabledSites(IEnvironmentProvider eProvider) {
			return GetSites(eProvider).Where(a => !a.Disabled);
		}

        public ITestSite GetTestSite(string id, string name, string domain, string systemID, bool disabled, Dictionary<string, object> properties, IEnumerable<ITestEnvironment> envs) {
            return (ITestSite)new DefaultTestSite(id, name, domain, systemID, disabled, properties, envs);
        }
	}
}
