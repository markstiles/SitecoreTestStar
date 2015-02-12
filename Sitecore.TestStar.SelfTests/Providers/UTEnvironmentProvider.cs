using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Extensions;
using Sitecore.TestStar.Core.Utility;
using Cons = Sitecore.TestStar.Core.Utility.Constants;
using Sitecore.TestStar.Core.Providers.Interfaces;
using Sitecore.TestStar.Core.Entities.Interfaces;
using Sitecore.Configuration;

namespace Sitecore.TestStar.Core.Providers {
	public class UTEnvironmentProvider : IEnvironmentProvider {

		public IEnumerable<ITestEnvironment> GetEnvironments() {
            
			IEnumerable<ITestEnvironment> environments = new List<ITestEnvironment>(){ 
                GetTestEnvironment(
                    "1", 
                    "TestEnvironment", 
                    "http://test.", 
                    string.Empty
                )
            };
			return environments;
		}

        public ITestEnvironment GetTestEnvironment(string id, string name, string domainPrefix, string ipAddress) {
            return (ITestEnvironment)new DefaultTestEnvironment(id, name, domainPrefix, ipAddress);
        }
	}
}
