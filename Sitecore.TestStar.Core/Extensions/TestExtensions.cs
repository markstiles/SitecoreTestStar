using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Core;

namespace Sitecore.TestStar.Core.Extensions {
	public static class TestExtensions {

		#region Test Methods

		/// <summary>
		/// This is used to pass in values to the test methods which has no parameters so it has contextual awareness
		/// </summary>
		public static void SetProperty(this TestMethod method, string key, object value) {
			if (!method.Properties.Contains(key))
				method.Properties.Add(key, value);
			else
				method.Properties[key] = value;
		}

		public static T GetProperty<T>(this TestMethod t, string key) {
			if (t.Properties.Contains(key))
				return (T)t.Properties[key];
			return default(T);
		}

		#endregion Test Methods

		#region Tests

		public static void SetProperty(this Test t, string key, object value) {
			if (!t.Properties.Contains(key))
				t.Properties.Add(key, value);
			else
				t.Properties[key] = value;
		}

		public static T GetProperty<T>(this ITest t, string key) {
			if (t.Properties.Contains(key))
				return (T)t.Properties[key];
			return default(T);
		}

		#endregion Tests

		#region Test Suites

        public static IEnumerable<TestMethod> GetMethodsByCategory(this TestSuite suite, string category) {
            return suite.GetFixtures()
                .SelectMany<TestFixture, TestMethod>(tf => tf.Tests.Cast<TestMethod>()
                    .Where(tm =>
                        tf.Categories().Any(c => c.Equals(category)) // if category is on the TestFixture
                        ||
                        tm.Categories().Any(c => c.Equals(category)) // if category is on the Test method
                    )
                )
                .Distinct();
        }

		/// <summary>
		/// Gets all the test fixtures
		/// </summary>
		private static IEnumerable<TestFixture> GetFixtures(this TestSuite suite) {
			List<TestFixture> fixtures = new List<TestFixture>();
			foreach (Test ts in suite.Tests) {
				if (ts is NamespaceSuite)
					fixtures.AddRange(GetFixtures((TestSuite)ts));
				else if (ts is TestFixture) {
					fixtures.Add((TestFixture)ts);
				}
			}
			return fixtures;
		}

		/// <summary>
		/// Gets all the categories for a test and children recursively
		/// </summary>
		public static IEnumerable<string> GetAllCategories(this Test suite) {
			List<string> cats = new List<string>();
            cats.AddRange(Categories(suite));
            
			if(suite.Tests != null)
				foreach (Test ts in suite.Tests)
					cats.AddRange(GetAllCategories(ts));

			return cats;
		}

		/// <summary>
		/// Gets all the categories for the current test
		/// </summary>
		private static IEnumerable<string> Categories(this Test suite) {
			List<string> cats = new List<string>();
			if (suite.Categories != null && suite.Categories.Count > 0)
				foreach (string c in suite.Categories)
					if (!cats.Contains(c))
						cats.Add(c);

			return cats;
		}

		#endregion Test Suites
	}
}
