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
		
		#region Template IDs

		public static readonly string SiteTemplate				= "{2FDA2A39-357F-42F0-B97C-DEED4F935F7B}";
		public static readonly string SystemTemplate			= "{F94502A7-D76E-468C-8488-3397368AFDD2}";
		public static readonly string ResultsFolderTemplate		= "{98CDE7D8-3488-43B6-8773-D140DDEBAB51}";
		public static readonly string ResultsListTemplate		= "{1915F851-227B-430C-88D0-373B85AFFBEE}";
		public static readonly string UnitTestResultTemplate	= "{6BBECB0C-2A2D-4BE6-B9D2-24D17104A2C5}";
		public static readonly string WebTestResultTemplate		= "{09F4C9EF-7E9E-4980-80EE-6E520A24332E}";

		#endregion Template IDs

		#region Item IDs

		public static readonly string EnvironmentFolder = "{3449A8FD-6516-4011-B0C5-46DCC4DCA208}";
		public static readonly string SiteFolder		= "{6B336815-7E53-454A-817E-FA52E4D6F33E}";
		public static readonly string ResultsFolder		= "{2789CBEA-390F-4EB3-8FE0-1752A800E720}";
		public static readonly string UnitAssemblies	= "{AE46FAEF-CAF6-45EA-9B52-516F1CCA5919}";
		public static readonly string WebAssemblies		= "{5AECEFE8-0A4E-4FF6-B498-B340B1050D0E}";
		public static readonly string TextDictionary	= "{EB0E6702-40D8-4DA1-B276-C3771F24F9E5}";

		#endregion Item IDs
	}
}
