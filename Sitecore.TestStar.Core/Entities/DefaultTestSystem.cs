using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.TestStar.Core.Providers;
using Sitecore.TestStar.Core.Utility;
using Sitecore.TestStar.Core.Entities.Interfaces;

namespace Sitecore.TestStar.Core.Entities {
	
	public class DefaultTestSystem : ITestSystem {

        protected string _ID;
		public string ID { get { return _ID; } set { _ID = value; } }
        protected string _Name;
        public string Name { get { return _Name; } set { _Name = value; } }
        private ISiteProvider SiteProvider;

		public DefaultTestSystem(string id, string name, ISiteProvider s) {
            if (s == null)
                throw new NullReferenceException();
            SiteProvider = s;
			ID = id;
			Name = name;
		}

		public virtual IEnumerable<ITestSite> Sites() {
            return SiteProvider.GetSites().Where(s => s.SystemID.Equals(this.ID)); 
		}
	}
}
