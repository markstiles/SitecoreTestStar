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
		public static Item GetItemByID(this Database db, string id) {
			return db.GetItem(SitecoreUtility.GetID(id));
		}

		public static string GetSafeFieldValue(this Item i, string fieldName){
            if (i == null)
                return string.Empty;
            Field f = i.Fields[fieldName];
			return (f == null) ? string.Empty : f.Value;
		}

		public static bool GetSafeFieldBool(this Item i, string fieldName) {
            if (i == null)
                return false; 
            return GetSafeFieldBool(i, fieldName, false);
		}
		public static bool GetSafeFieldBool(this Item i, string fieldName, bool defaultValue) {
            if (i == null)
                return defaultValue; 
            CheckboxField f = i.Fields[fieldName];
			return (f == null) ? defaultValue : f.Checked;
		}

		public static DateTime GetSafeDateFieldValue(this Item i, string fieldName) {
            if (i == null)
                return DateTime.Now; 
            DateField f = i.Fields[fieldName];
			return (f == null) ? DateTime.Now : f.DateTime;
		}

		public static string ToDateFieldValue(this DateTime Date) {
			return Date.ToString("yyyyMMddTHHmmss");
		}
	}
}
