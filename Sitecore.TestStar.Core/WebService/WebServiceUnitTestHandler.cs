﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Core;
using Sitecore.TestStar.Core.Managers;
using Sitecore.TestStar.Core.Utility;

namespace Sitecore.TestStar.Core.WebService {
	public class WebServiceUnitTestHandler : IUnitTestHandler {

		#region ITestHandler Events

		public void OnError(TestMethod tm, TestResult tr) {
			Results(tm, "Has Errors", tr.Message, "err", true);
		}

		public void OnFailure(TestMethod tm, TestResult tr) {
			Results(tm, "Failed", tr.Message, "fail", true);
		}

		public void OnSuccess(TestMethod tm, TestResult tr) {
			Results(tm, "Succeeded", string.Empty, "pass", false);
		}

		#endregion ITestHandler Events

		#region Messaging

        public List<JSONUnitTestResult> ResultList = new List<JSONUnitTestResult>();

		private bool ResultFlag = false;

		/// <summary>
		/// writes message to the results window
		/// </summary>
		protected void Results(TestMethod tm, string name, string message, string type, bool failed) {

			JSONUnitTestResult r = new JSONUnitTestResult(
				ResultFlag, 
				type,
				(tm != null) ? TestUtility.GetClassName(tm.MethodName) : string.Empty,
				name,
				message,
				failed
			);
			ResultList.Add(r);

			ResultFlag = !ResultFlag;
		}

		#endregion Messaging
	}
}
