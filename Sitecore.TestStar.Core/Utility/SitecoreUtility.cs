using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.Core.Utility {
	public static class SitecoreUtility {

		public static Sitecore.Data.ID GetID(string id){
			return (Sitecore.Data.ID.IsID(id))
				? Sitecore.Data.ID.Parse(id)
				: Sitecore.Data.ID.Null;
		}
	}
}
