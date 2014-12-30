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

		#endregion DB

		#region Template IDs

		public static readonly string SiteTemplate = "{2FDA2A39-357F-42F0-B97C-DEED4F935F7B}";
		public static readonly string SystemTemplate = "{F94502A7-D76E-468C-8488-3397368AFDD2}";
		public static readonly string ResultsFolderTemplate = "{98CDE7D8-3488-43B6-8773-D140DDEBAB51}";
		public static readonly string ResultsTemplate = "{1915F851-227B-430C-88D0-373B85AFFBEE}";

		#endregion Template IDs

		#region Item IDs

		public static readonly string EnvironmentFolder = "{3449A8FD-6516-4011-B0C5-46DCC4DCA208}";
		public static readonly string SiteFolder = "{6B336815-7E53-454A-817E-FA52E4D6F33E}";
		public static readonly string ResultsFolder = "{2789CBEA-390F-4EB3-8FE0-1752A800E720}";
		public static readonly string UnitAssemblies = "{AE46FAEF-CAF6-45EA-9B52-516F1CCA5919}";
		public static readonly string WebAssemblies = "{5AECEFE8-0A4E-4FF6-B498-B340B1050D0E}";

		#endregion Item IDs

		#region Exception Messages

		public static class Exceptions {
			public static readonly string EnvFoldNull = "Sitecore.TestStar.Core.EnvironmentProvider.GetEnvironments: Check that the Environments folder exists in the content tree.";
			public static readonly string SiteFoldNull = "Sitecore.TestStar.Core.SiteProvider.GetSites: Check that the Sites folder exists in the content tree.";
			public static readonly string UnitFoldNull = "Sitecore.TestStar.Core.AssemblyProvider.GetUnitTestAssemblies: Check that the UnitTestAssemblies folder exists in the content tree.";
			public static readonly string WebFoldNull = "Sitecore.TestStar.Core.AssemblyProvider.GetWebTestAssemblies: Check that the WebTestAssemblies folder exists in the content tree.";
			public static readonly string NullJSON = "Sitecore.TestStar.Core.Utility.JsonSerializer.GetObject: The data is empty.";
		}
		
		#endregion Exception Messages
	}
}
