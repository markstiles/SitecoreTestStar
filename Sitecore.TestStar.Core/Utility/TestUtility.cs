using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using NUnit.Core;

namespace Sitecore.TestStar.Core.Utility {
	public class TestUtility {
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
