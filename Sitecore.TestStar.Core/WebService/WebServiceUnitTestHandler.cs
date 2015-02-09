using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Core;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Extensions;
using Sitecore.TestStar.Core.Managers;
using Sitecore.TestStar.Core.Utility;

namespace Sitecore.TestStar.Core.WebService {
	public class WebServiceUnitTestHandler : IUnitTestHandler {

		#region Messaging

		public List<DefaultUnitTestResult> ResultList = new List<DefaultUnitTestResult>();

		#endregion Messaging

		#region ITestHandler Events

		public void OnResult(TestMethod tm, TestResult tr, TestResultEnum tre) {

			DefaultUnitTestResult utr = new DefaultUnitTestResult(
				string.Empty,
				DateTime.Now,
				tre.ToString(),
				TestUtility.GetClassName(tm.MethodName),
				TestUtility.GetClassName(((Test)tm).ClassName),
				tr.Message
			);
            
			utr.ID = SitecoreUtility.CreateResultEntry(tm.FixtureType.FullName, utr.Date.ToDateFieldValue(), utr.ClassName, utr.Method, utr.Type, utr.Message, true, string.Empty, string.Empty, string.Empty, string.Empty);
			ResultList.Add(utr);
		}

		#endregion ITestHandler Events
	}
}
