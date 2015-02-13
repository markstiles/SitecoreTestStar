using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Sitecore.Data;

namespace Sitecore.TestStar.Core.Utility {
	public static class Constants {

		#region Contextual Awareness

		/// <summary>
		/// this section switches contexts based on the web app vs the console app
		/// </summary>
		public static bool IsWebApp {
			get {
				return (HttpContext.Current != null && HttpContext.Current.Request != null);
			}
		}

		public static string ApplicationRoot {
			get {
				return (IsWebApp)
					? HttpContext.Current.Request.PhysicalApplicationPath
					: AppDomain.CurrentDomain.BaseDirectory.Split(new string[] {"bin"}, StringSplitOptions.RemoveEmptyEntries)[0];
			}
		}

		public static string ExecutionRoot {
			get {
				return (IsWebApp)
					? string.Format("{0}bin", HttpContext.Current.Request.PhysicalApplicationPath)
					: string.Format("{0}bin", AppDomain.CurrentDomain.BaseDirectory);
			}
		}

		#endregion 

		#region DB

		public static Database MasterDB {
			get {
				return Sitecore.Configuration.Factory.GetDatabase("master");
			}
		}

		public static Database WebDB {
			get {
				return Sitecore.Configuration.Factory.GetDatabase("web");
			}
		}

		#endregion DB
	}
}
