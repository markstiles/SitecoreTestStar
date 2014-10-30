using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.TestStar.Core.Providers;
using Sitecore.TestStar.Core.Utility;

namespace Sitecore.TestStar.Core.Entities {
	
	public class TestSystem {
		
		public int ID = -1; 
		public string Name;
		public virtual IEnumerable<TestSite> Sites {
			get {
				return SiteProvider.GetSites().Where(s => s.SystemID.Equals(this.ID)); 
			}
		}
	}
}
