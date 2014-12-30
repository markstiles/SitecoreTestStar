using NUnit.Core;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Managers;
using Sitecore.TestStar.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.Core.WebService {
    public class WebServiceWebTestHandler : IWebTestHandler {
        
        #region ITestHandler Events

        public void OnError(TestMethod tm, TestEnvironment te, TestSite ts, TestResult tr, string requestURL, HttpStatusCode responseStatus) {
			Results(ts, te, tm, tr, "Has Errors", tr.Message, "err", true, requestURL, responseStatus);
        }

        public void OnFailure(TestMethod tm, TestEnvironment te, TestSite ts, TestResult tr, string requestURL, HttpStatusCode responseStatus) {
			Results(ts, te, tm, tr, "Failed", tr.Message, "fail", true, requestURL, responseStatus);
        }

        public void OnSuccess(TestMethod tm, TestEnvironment te, TestSite ts, TestResult tr, string requestURL, HttpStatusCode responseStatus) {
			Results(ts, te, tm, tr, "Succeeded", string.Empty, "pass", false, requestURL, responseStatus);
        }

        public void OnSkipped(TestMethod tm, TestEnvironment te, TestSite ts) {
			Results(ts, te, tm, null, "Skipped", string.Format("{0} doesn't support the {1} environment", ts.Name, te.Name), "skip", false, string.Empty, HttpStatusCode.Unused);
        }

        #endregion ITestHandler Events

        #region Messaging

        public List<JSONWebTestResult> ResultList = new List<JSONWebTestResult>();

        private bool ResultFlag = false;

        /// <summary>
        /// writes message to the results window
        /// </summary>
        protected void Results(TestSite ts, TestEnvironment te, TestMethod tm, TestResult tr, string name, string message, string type, bool failed, string requestURL, HttpStatusCode responseStatus) {

			SitecoreUtility.AddWebTestResults(TestUtility.GetClassName(((Test)tm).ClassName), requestURL, responseStatus, (tr != null) ? tr.Message : string.Empty);
			
            JSONWebTestResult r = new JSONWebTestResult(
                ResultFlag,
                type,
                (tm != null) ? TestUtility.GetClassName(tm.ClassName) : string.Empty,
                name,
                message,
                ts.Name,
                te.Name,
				failed
            );
            ResultList.Add(r);

            ResultFlag = !ResultFlag;
        }

        #endregion Messaging
    }
}
