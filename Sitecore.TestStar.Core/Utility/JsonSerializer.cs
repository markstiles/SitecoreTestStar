using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using Cons = Sitecore.TestStar.Core.Utility.Constants;

namespace Sitecore.TestStar.Core.Utility {
	public static class JsonSerializer {

		public static T GetObject<T>(string data) {
			if(string.IsNullOrEmpty(data))
				throw new Exception(Cons.Exceptions.NullJSON);

			T results = new JavaScriptSerializer().Deserialize<T>(data);
			return results;
		}

		/// <summary>
		/// this is used to convert one type of entity into a sub/super class
		/// </summary>
		public static T ConvertTo<T>(object o) {
			string data = new JavaScriptSerializer().Serialize(o);
			T results = new JavaScriptSerializer().Deserialize<T>(data);
			return results;
		}

		public static string GetJSON<T>(T contentObj) {
			return new JavaScriptSerializer().Serialize(contentObj);
		}
	}
}
