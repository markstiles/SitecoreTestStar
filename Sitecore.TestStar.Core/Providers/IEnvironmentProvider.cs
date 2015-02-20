using Sitecore.Data.Items;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.Core.Providers {
    public interface IEnvironmentProvider {

        IEnumerable<ITestEnvironment> GetEnvironments();

        ITestEnvironment GetTestEnvironment(string id, string name, string domainPrefix, string ipAddress);
    }
}
