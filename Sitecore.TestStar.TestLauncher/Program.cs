using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Core;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Extensions;
using Sitecore.TestStar.Core.Managers;
using Sitecore.TestStar.Core.Providers;
using Sitecore.TestStar.Core.Utility;
using Sitecore.TestStar.TestLauncher.Handlers;

namespace Sitecore.TestStar.TestLauncher {

	public enum ExitCode : int {
		Success = 0,
		UnitTestFailed = 2,
		UnitTestException = 4,
		WebTestFailed = 8,
		WebTestException = 16
	}

	public class Program {

		public static List<string> GetStrings(string param) {
			return param.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
		}

		static void Main(string[] args) {

			//params 0 = determines test type
			if (args.Length > 0) {
				if (args[0] == "-w")
					RunWebTest(args);
				else if (args[0] == "-u")
					RunUnitTest(args);
			} else {
				Console.WriteLine("You need to specify a test type.");
				return;
			}
		}

		static void RunUnitTest(string[] args) {

			//params 1 = test assembly
			string testAssembly = string.Empty;
			if (args.Length > 1) {
				testAssembly = args[1];
			} else {
				Console.WriteLine("You need to specify an assembly.");
				return;
			}

			//params 2 = test category
			List<string> categories = new List<string>();
			if (args.Length > 2) {
				foreach (string s in GetStrings(args[2])) {
					if (!categories.Contains(s)) {
						Console.WriteLine(string.Format("Caught '{0}' Category.", s));
						categories.Add(s);
					}
				}
			}

			//params 3 = test method
			List<string> names = new List<string>();
			if (args.Length > 3) {
				foreach (string n in GetStrings(args[3])) {
					if (!names.Contains(n)) {
						Console.WriteLine(string.Format("Caught '{0}' Method.", n));
						names.Add(n);
					}
				}
			}

			//setup for testing
			CoreExtensions.Host.InitializeService();
			//get the test suite
			TestSuite suite = TestUtility.GetTestSuite(testAssembly);

			Dictionary<string, TestMethod> Methods = new Dictionary<string, TestMethod>();
			IEnumerable<TestMethod> allMethods = suite.GetMethods();
			if (!categories.Any() && !names.Any()) { // if nothing selected add all
				Methods = allMethods.ToDictionary(a => a.MethodName);
			} else { // add one at a time
				IEnumerable<TestFixture> Fixtures = suite.GetFixtures();
				foreach (string c in categories) {
					foreach (TestFixture tf in Fixtures) {
						bool fixtHasCat = (tf.Categories().Any(b => b.Equals(c)));
						foreach (TestMethod tm in tf.Tests) {
							//if fixture or the method has the selected category then add
							if (fixtHasCat || tm.Categories().Any(b => b.Equals(c))) {
								Console.WriteLine(string.Format("Adding '{0}' Method.", tm.MethodName));
								Methods.Add(tm.MethodName, tm);
							}
						}
					}
				}

				foreach (string n in names) {
					foreach (TestMethod ctm in allMethods.Where(a => a.MethodName.Equals(n))) {
						if (!Methods.ContainsKey(ctm.MethodName)) {
							Console.WriteLine(string.Format("Adding '{0}' Method.", ctm.MethodName));
							Methods.Add(ctm.MethodName, ctm);
						}
					}
				}
			}

			if (!Methods.Any()) {
				Console.WriteLine("There are no Test Methods found. Make sure the class method has the [Test] attribute.");
				return;
			}
			UnitTestManager manager = new UnitTestManager(new UnitConsoleTestHandler());
			foreach (TestMethod tm in Methods.Values)
				manager.RunTest(tm);
		}

		static void RunWebTest(string[] args) {

			//params 1 = test assembly
			string testAssembly = string.Empty;
			if (args.Length > 1) {
				testAssembly = args[1];
			} else {
				Console.WriteLine("You need to specify an assembly.");
				return;
			}

			//params 2 = test name
			string testName = string.Empty;
			if (args.Length > 2) {
				testName = args[2];
			} else {
				Console.WriteLine("You need to specify a test.");
				return;
			}

			//params 3 = environments
			Dictionary<string, TestEnvironment> Environments = new Dictionary<string, TestEnvironment>();
			if (args.Length > 3) {
				IEnumerable<TestEnvironment> prEnv = EnvironmentProvider.GetEnvironments();
				foreach (string s in GetStrings(args[3])) {
					foreach (TestEnvironment fenv in prEnv.Where(a => a.ID.Equals(int.Parse(s)))) {
						if (!Environments.ContainsKey(fenv.ID)) {
							Console.WriteLine(string.Format("Adding '{0}' Environment.", fenv.Name));
							Environments.Add(fenv.ID, fenv);
						}
					}
				}
			}

			// params 4 = systems
			// params 5 = sites 
			// will look for sites by system unless systems is an empty string then it looks for them by site
			Dictionary<string, TestSite> Sites = new Dictionary<string, TestSite>();
			IEnumerable<TestSite> prSites = SiteProvider.GetEnabledSites();
			if (args.Length > 4 && !string.IsNullOrEmpty(args[4])) {
				foreach (string s in GetStrings(args[4])) {
					foreach (TestSite fsite in prSites.Where(a => a.SystemID.Equals(int.Parse(s)))) {
						if (!Sites.ContainsKey(fsite.ID)) {
							Console.WriteLine(string.Format("Adding '{0}' Site.", fsite.Name));
							Sites.Add(fsite.ID, fsite);
						}
					}
				}
			}
			if (args.Length > 5) {
				foreach (string s in GetStrings(args[5])) {
					foreach (TestSite fsite in prSites.Where(a => a.ID.Equals(int.Parse(s)))) {
						if (!Sites.ContainsKey(fsite.ID)) {
							Console.WriteLine(string.Format("Adding '{0}' Site.", fsite.Name));
							Sites.Add(fsite.ID, fsite);
						}
					}
				}
			}

			//setup for testing
			CoreExtensions.Host.InitializeService();
			//get the test suite
			TestSuite suite = TestUtility.GetTestSuite(testAssembly);

			IEnumerable<TestFixture> Fixtures = suite.GetFixtures().Where(a => a.ClassName.EndsWith(string.Format(".{0}", testName)));
			if (!Fixtures.Any()) {
				Console.WriteLine("There were no Test Fixtures found. Make sure the class has the [TestFixture] attribute.");
				return;
			}
			TestFixture tf = Fixtures.First();
			WebTestManager manager = new WebTestManager(new WebConsoleTestHandler());
			manager.RunTest(tf, Environments.Values, Sites.Values);
		}
	}
}
