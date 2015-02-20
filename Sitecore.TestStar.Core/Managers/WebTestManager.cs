using NUnit.Core;
using NUnit.Util;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Entities.Interfaces;
using Sitecore.TestStar.Core.Extensions;
using Sitecore.TestStar.Core.Providers;
using Sitecore.TestStar.Core.Tests;
using Sitecore.TestStar.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;

namespace Sitecore.TestStar.Core.Managers {
	public class WebTestManager {

        #region Messaging

        public List<DefaultWebTestResult> ResultList = new List<DefaultWebTestResult>();

        #endregion Messaging

        private ITextEntryProvider TextProvider;

		public WebTestManager(ITextEntryProvider t){
            if (t == null)
                throw new NullReferenceException();
            TextProvider = t;
        }

        public void RunTest(TestMethod tm, ITestEnvironment Environment, ITestSite Site) {
            IEnumerable<ITestEnvironment> Environments = new List<ITestEnvironment>() { Environment };
            IEnumerable<ITestSite> Sites = new List<ITestSite>() { Site };
            RunTest(tm, Environments, Sites);
        }

		public void RunTest(TestMethod tm, IEnumerable<ITestEnvironment> Environments, IEnumerable<ITestSite> Sites) {
			if (tm == null)
                throw new NullReferenceException(TextProviderPaths.Exceptions.Managers.TestMethodNull(TextProvider));
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

					var t = new Thread(new ThreadStart(() => HandleTest(tm, te, ts)));
					t.SetApartmentState(ApartmentState.STA);
					t.Start();
					t.Join();
				}
			}
		}

		/// <summary>
		/// Passes the proper test result to the handler method
		/// </summary>
		private void HandleTest(TestMethod tm, ITestEnvironment te, ITestSite ts) {
			
			TestResult tr = tm.Run(new NullListener(), TestFilter.Empty);
			ResultSummarizer summ = new ResultSummarizer(tr);

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
                tm.FixtureType.FullName,
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

            ResultList.Add(wtr);
        }
	}
}
