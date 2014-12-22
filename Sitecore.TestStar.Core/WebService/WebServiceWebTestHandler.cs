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
            Results(ts, te, tm, "Has Errors", tr.Message, "err");
        }

        public void OnFailure(TestMethod tm, TestEnvironment te, TestSite ts, TestResult tr, string requestURL, HttpStatusCode responseStatus) {
            Results(ts, te, tm, "Failed", tr.Message, "fail");
        }

        public void OnSuccess(TestMethod tm, TestEnvironment te, TestSite ts, TestResult tr, string requestURL, HttpStatusCode responseStatus) {
            Results(ts, te, tm, "Succeeded", string.Empty, "pass");
        }

        public void OnSkipped(TestMethod tm, TestEnvironment te, TestSite ts) {
            Results(ts, te, tm, "Skipped", string.Format("{0} doesn't support the {1} environment", ts.Name, te.Name), "skip");
        }

        #endregion ITestHandler Events

        #region Messaging

        public List<JSONWebTestResult> ResultList = new List<JSONWebTestResult>();

        private bool ResultFlag = false;

        /// <summary>
        /// writes message to the results window
        /// </summary>
        protected void Results(TestSite ts, TestEnvironment te, TestMethod tm, string name, string value, string type) {

            JSONWebTestResult r = new JSONWebTestResult(
                ResultFlag,
                type,
                (tm != null) ? TestUtility.GetClassName(tm.ClassName) : string.Empty,
                name,
                value,
                ts.Name,
                te.Name
            );
            ResultList.Add(r);

            ResultFlag = !ResultFlag;
        }

        #endregion Messaging
    }
}
