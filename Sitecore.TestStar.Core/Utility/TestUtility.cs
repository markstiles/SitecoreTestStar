using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using NUnit.Core;
using Sitecore.TestStar.Core.Extensions;
using Sitecore.TestStar.Core.Providers;

namespace Sitecore.TestStar.Core.Utility {
	public class TestUtility {

        #region Unit Tests

		public static Dictionary<string, TestSuite> GetUnitTestSuites() {
			Dictionary<string, TestSuite> Suites = new Dictionary<string, TestSuite>();
			// Find tests in current assembly
			foreach (string a in AssemblyProvider.GetUnitTestAssemblies()) {
				if (!Suites.ContainsKey(a))
					Suites.Add(a, TestUtility.GetTestSuite(a));
			}
			return Suites;
		}

        #endregion Unit Tests

        #region Web Tests

        public static Dictionary<string, TestSuite> GetWebTestSuites() {
            Dictionary<string, TestSuite> Suites = new Dictionary<string, TestSuite>();
            // Find tests in current assembly
            foreach (string a in AssemblyProvider.GetWebTestAssemblies()) {
                if(!Suites.ContainsKey(a))
					Suites.Add(a, TestUtility.GetTestSuite(a));
            }
            return Suites;
        }

        #endregion Web Tests

        public static TestSuite GetTestSuite(string assemblyName) {	
			TestSuiteBuilder builder = new TestSuiteBuilder();
			string packagePath = string.Format(@"{0}\{1}.dll", Constants.ExecutionRoot, assemblyName);
			TestPackage testPackage = new TestPackage(packagePath);
			TestSuite suite = builder.Build(testPackage);
			return suite;
		}

		/// <summary>
		/// gets the class name from the fully qualified class path
		/// </summary>
		public static string GetClassName(string classPath) {
			return (!string.IsNullOrEmpty(classPath) && classPath.Contains(".")) ? classPath.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries).Last() : classPath;
		}
	}
}
