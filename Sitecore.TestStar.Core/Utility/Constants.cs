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
					: AppDomain.CurrentDomain.BaseDirectory;
			}
		}

		#endregion 

		#region Assembly Info

		public static string DefaultWebTestAssembly {
			get {
				return Sitecore.Configuration.Settings.GetSetting("TestStar.DefaultWebTestAssembly");
			}
		}

		public static string DefaultUnitTestAssembly {
			get {
				return Sitecore.Configuration.Settings.GetSetting("TestStar.DefaultUnitTestAssembly");
			}
		}

		public static string DefaultTestLauncher {
			get {
				return Sitecore.Configuration.Settings.GetSetting("TestStar.DefaultTestLauncher");
			}
		}

		#endregion Assembly Info

		#region DB

		public static Database MasterDB {
			get {
				return Sitecore.Configuration.Factory.GetDatabase("master");
			}
		}

		#endregion DB

		#region Item IDs

		public static readonly string EnvironmentFolder = "{3449A8FD-6516-4011-B0C5-46DCC4DCA208}";
		public static readonly string SiteFolder = "{6B336815-7E53-454A-817E-FA52E4D6F33E}";
		public static readonly string SystemFolder = "{6BCA3016-6B6F-4D95-BC73-6351CB8A5EA4}";
		public static readonly string ResultsFolder = "{2789CBEA-390F-4EB3-8FE0-1752A800E720}";

		#endregion Item IDs

		#region Exception Messages

		public static class Exceptions {
			public static readonly string EnvFoldNull = "Sitecore.TestStar.Core.EnvironmentProvider.GetEnvironments: Check that the environment folder exists in the content tree.";
			public static readonly string SiteFoldNull = "Sitecore.TestStar.Core.SystemProvider.GetSites: Check that the site folder exists in the content tree.";
			public static readonly string SysFoldNull = "Sitecore.TestStar.Core.SystemProvider.GetSystems: Check that the system folder exists in the content tree.";
		}
		
		#endregion Exception Messages
	}
}
