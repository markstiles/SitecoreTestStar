using NUnit.Core;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Entities.Interfaces;
using Sitecore.TestStar.Core.Extensions;
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

		#region Messaging

		public List<WebTestResult> ResultList = new List<WebTestResult>();

		#endregion Messaging

        #region ITestHandler Events

		public void OnResult(TestMethod tm, ITestEnvironment te, ITestSite ts, TestResult tr, string requestURL, HttpStatusCode responseStatus, TestResultEnum tre) {

			WebTestResult wtr = new WebTestResult(
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
        	
        #endregion ITestHandler Events
    }
}
