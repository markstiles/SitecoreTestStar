using Sitecore.Data.Items;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.Core.Providers.Interfaces {
    public interface ISiteProvider {
        
        IEnumerable<ITestSite> GetSites(IEnvironmentProvider eProvider);
		
        IEnumerable<ITestSite> GetEnabledSites(IEnvironmentProvider eProvider);

        ITestSite GetTestSite(string id, string name, string domain, string systemID, bool disabled, Dictionary<string, object> properties, IEnumerable<ITestEnvironment> envs);
    }
}
