using Sitecore.TestStar.Core.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.Core.Entities.Interfaces {
    public interface ITestSystem {

        string ID { get; set; }
        string Name { get; set; }

        IEnumerable<ITestSite> Sites();
    }
}
