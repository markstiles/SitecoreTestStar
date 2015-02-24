using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.Core.Providers {
    public interface ISiteProvider {
        
        IEnumerable<ITestSite> GetSites();
		
        IEnumerable<ITestSite> GetEnabledSites();

        ITestSite GetTestSite(string id, string name, string domain, string systemID, bool disabled, Dictionary<string, string> properties, IEnumerable<ITestEnvironment> envs);
    }
}
