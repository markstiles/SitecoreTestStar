using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.TestStar.Core.Providers;
using Sitecore.TestStar.Core.Utility;
using Sitecore.TestStar.Core.Providers.Interfaces;
using Sitecore.TestStar.Core.Entities.Interfaces;

namespace Sitecore.TestStar.Core.Entities {
	
	public class TestSystem : ITestSystem {

        protected string _ID;
		public string ID { get { return _ID; } set { _ID = value; } }
        protected string _Name;
        public string Name { get { return _Name; } set { _Name = value; } }

		public TestSystem(string id, string name) {
			ID = id;
			Name = name;
		}

		public virtual IEnumerable<ITestSite> Sites(ISiteProvider sProvider, IEnvironmentProvider eProvider) {
            return sProvider.GetSites(eProvider).Where(s => s.SystemID.Equals(this.ID)); 
		}
	}
}
