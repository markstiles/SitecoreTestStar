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
using Sitecore.TestStar.Core.Providers;
using Sitecore.TestStar.Core.Tests;
using Sitecore.TestStar.Core.Utility;
using Cons = Sitecore.TestStar.Core.Utility.Constants;
using Sitecore.TestStar.Core.Entities.Interfaces;

namespace Sitecore.TestStar.Core.Managers {
	public class WebTestManager {

        #region Messaging

        public List<DefaultWebTestResult> ResultList = new List<DefaultWebTestResult>();

        #endregion Messaging

		public WebTestManager(){ }

        public void RunTest(TestFixture tf, ITestEnvironment Environment, ITestSite Site) {
            IEnumerable<ITestEnvironment> Environments = new List<ITestEnvironment>() { Environment };
            IEnumerable<ITestSite> Sites = new List<ITestSite>() { Site };
            RunTest(tf, Environments, Sites);
        }

		public void RunTest(TestFixture tf, IEnumerable<ITestEnvironment> Environments, IEnumerable<ITestSite> Sites) {
			if (tf == null)
				throw new NullReferenceException(SCTextEntryProvider.Exceptions.Managers.TestFixtureNull);
			TestMethod tm = tf.GetMethod("RunTest");
			if (tm == null)
				throw new NullReferenceException(SCTextEntryProvider.Exceptions.Managers.TestMethodNull);
			foreach (ITestEnvironment te in Environments) {
				foreach (ITestSite ts in Sites) {
					if (!ts.Environments.Any(en => en.ID.Equals(te.ID))) {
						OnResult(tm, te, ts, null, string.Empty, HttpStatusCode.NoContent, TestResultEnum.Skipped);
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
		/// Passes the proper test result to the handler method
		/// </summary>
		private void HandleTest(TestFixture tf, TestMethod tm, ITestEnvironment te, ITestSite ts) {
			
			TestResult tr = tm.Run(new NullListener(), TestFilter.Empty);
			ResultSummarizer summ = new ResultSummarizer(tr);

			Type t = tf.FixtureType;
			HttpStatusCode status = ((Test)tm).GetProperty<HttpStatusCode>(BaseWebTest.ResponseStatusCodeKey);
			string requestURL = ((Test)tm).GetProperty<string>(BaseWebTest.RequestURLKey);
			if (tr.IsError) {
				OnResult(tm, te, ts, tr, requestURL, status, TestResultEnum.Error);
			} else if (tr.IsFailure) {
				OnResult(tm, te, ts, tr, requestURL, status, TestResultEnum.Failure);
			} else if (tr.IsSuccess) {
				OnResult(tm, te, ts, tr, requestURL, status, TestResultEnum.Success);
			}
		}

        private void OnResult(TestMethod tm, ITestEnvironment te, ITestSite ts, TestResult tr, string requestURL, HttpStatusCode responseStatus, TestResultEnum tre) {

            DefaultWebTestResult wtr = new DefaultWebTestResult(
                string.Empty,
                DateTime.Now,
                tre.ToString(),
                TestUtility.GetClassName(tm.ClassName),
                TestUtility.GetClassName(((Test)tm).ClassName),
                (tr != null) ? tr.Message : string.Empty,
                ts.Name,
                te.Name,
                requestURL,
                responseStatus.ToString()
            );

            wtr.ID = SitecoreUtility.CreateResultEntry(tm.FixtureType.FullName, wtr.Date.ToDateFieldValue(), wtr.ClassName, wtr.Method, wtr.Type, wtr.Message, false, wtr.Site, wtr.Environment, wtr.RequestURL, wtr.ResponseStatus);
            ResultList.Add(wtr);
        }
	}
}
