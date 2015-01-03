using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Core;
using NUnit.Util;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Extensions;
using Sitecore.TestStar.Core.Tests;
using Sitecore.TestStar.Core.Utility;

namespace Sitecore.TestStar.Core.Managers {
	public class WebTestManager {

		private IWebTestHandler Handler;

		public WebTestManager(IWebTestHandler handler){
			if (handler == null)
				throw new ArgumentNullException("The handler provided is null");
			Handler = handler;
		}

        public void RunTest(TestFixture tf, TestEnvironment Environment, TestSite Site) {
            IEnumerable<TestEnvironment> Environments = new List<TestEnvironment>() { Environment };
            IEnumerable<TestSite> Sites = new List<TestSite>() { Site };
            RunTest(tf, Environments, Sites);
        }

		public void RunTest(TestFixture tf, IEnumerable<TestEnvironment> Environments, IEnumerable<TestSite> Sites) {
			if (tf == null)
				throw new NullReferenceException("Test Fixture was null. Make sure the class has the [TestFixture] attribute.");
			TestMethod tm = tf.GetMethod("RunTest");
			if (tm == null)
				throw new NullReferenceException("Test Method was null. Make sure the class method has the [Test] attribute.");
			foreach (TestEnvironment te in Environments) {
				foreach (TestSite ts in Sites) {
					if (!ts.Environments.Any(en => en.ID.Equals(te.ID))) {
						Handler.OnResult(tm, te, ts, null, string.Empty, HttpStatusCode.NoContent, TestResultEnum.Skipped);
						continue;
					}

					//set properties to be used during testing
					tm.SetProperty(BaseWebTest.RequestURLKey, ts.BaseURL(te));
					tm.SetProperty(BaseWebTest.EnvironmentKey, te);
					tm.SetProperty(BaseWebTest.SiteKey, ts);

					var t = new Thread(new ThreadStart(() => HandleTest(tf, tm, te, ts)));
					t.SetApartmentState(ApartmentState.STA);
					t.Start();
					t.Join();
				}
			}
		}

		/// <summary>
		/// deals with the results of tests
		/// </summary>
		private void HandleTest(TestFixture tf, TestMethod tm, TestEnvironment te, TestSite ts) {
			
			TestResult tr = tm.Run(new NullListener(), TestFilter.Empty);
			ResultSummarizer summ = new ResultSummarizer(tr);

			Type t = tf.FixtureType;
			HttpStatusCode status = ((Test)tm).GetProperty<HttpStatusCode>(BaseWebTest.ResponseStatusCodeKey);
			string requestURL = ((Test)tm).GetProperty<string>(BaseWebTest.RequestURLKey);
			if (tr.IsError) {
				Handler.OnResult(tm, te, ts, tr, requestURL, status, TestResultEnum.Error);
			} else if (tr.IsFailure) {
				Handler.OnResult(tm, te, ts, tr, requestURL, status, TestResultEnum.Failure);
			} else if (tr.IsSuccess) {
				Handler.OnResult(tm, te, ts, tr, requestURL, status, TestResultEnum.Success);
			}
		}
	}
}
