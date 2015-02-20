using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.TestStar.Core.Utility;
using Cons = Sitecore.TestStar.Core.Utility.Constants;
using Sitecore.TestStar.Core.Extensions;
using Sitecore.Configuration;

namespace Sitecore.TestStar.Core.Providers {
	public class UTAssemblyProvider : IAssemblyProvider {

        public static string UTTestAssemblyName = "Sitecore.TestStar.SelfTests";

        public IEnumerable<string> GetUnitTestAssemblies() {
            return new List<string>() { UTTestAssemblyName };
		}

        public IEnumerable<string> GetWebTestAssemblies() {
            return new List<string>() { UTTestAssemblyName };
		}
	}
}
