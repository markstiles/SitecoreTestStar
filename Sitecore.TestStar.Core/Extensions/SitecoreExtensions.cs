using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.TestStar.Core.Utility;
using Cons = Sitecore.TestStar.Core.Utility.Constants;

namespace Sitecore.TestStar.Core.Extensions {
	public static class SitecoreExtensions {
		public static Item GetItem(this Database db, string id) {
			return db.GetItem(SitecoreUtility.GetID(Cons.EnvironmentFolder));
		}

		public static string GetSafeFieldValue(this Item i, string fieldName){
			Field f = i.Fields[fieldName];
			return (f == null) ? string.Empty : f.Value;
		}

		public static bool GetSafeFieldBool(this Item i, string fieldName) {
			return GetSafeFieldBool(i, fieldName, false);
		}
		public static bool GetSafeFieldBool(this Item i, string fieldName, bool defaultValue) {
			CheckboxField f = i.Fields[fieldName];
			return (f == null) ? defaultValue : f.Checked;
		}
	}
}
