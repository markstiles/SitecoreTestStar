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

		public List<UnitTestResult> ResultList = new List<UnitTestResult>();

		#endregion Messaging

		#region ITestHandler Events

		public void OnResult(TestMethod tm, TestResult tr, TestResultEnum tre) {

			UnitTestResult utr = new UnitTestResult(
				string.Empty,
				tre.ToString(),
				TestUtility.GetClassName(tm.MethodName),
				TestUtility.GetClassName(((Test)tm).ClassName),
				tr.Message
			);

			utr.ID = SitecoreUtility.CreateResultEntry((tm.Categories().Any()) ? tm.Categories().First() : utr.ClassName, utr.ClassName, utr.Method, utr.Type, utr.Message, true, string.Empty, string.Empty, string.Empty, string.Empty);
			ResultList.Add(utr);
		}

		#endregion ITestHandler Events
	}
}
