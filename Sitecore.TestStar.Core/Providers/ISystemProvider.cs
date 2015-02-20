using Sitecore.Data.Items;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.Core.Providers {
    public interface ISystemProvider {

        IEnumerable<ITestSystem> GetSystems();

        ITestSystem GetTestSystem(string id, string name);
    }
}
