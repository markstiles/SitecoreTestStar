using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.Core.Providers {
    public interface IAssemblyProvider {

        IEnumerable<string> GetUnitTestAssemblies();
		
		IEnumerable<string> GetWebTestAssemblies();
    }
}
