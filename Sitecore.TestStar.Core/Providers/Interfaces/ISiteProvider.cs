using Sitecore.Data.Items;
using Sitecore.TestStar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.Core.Providers.Interfaces {
    public interface ISiteProvider {
        
        IEnumerable<TestSite> GetSites(IEnvironmentProvider eProvider);
		
        IEnumerable<TestSite> GetEnabledSites(IEnvironmentProvider eProvider);

        TestSite FillTestSite(IEnvironmentProvider eProvider, Item i);
    }
}
