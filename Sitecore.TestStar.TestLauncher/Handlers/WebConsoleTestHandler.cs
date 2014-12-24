using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NUnit.Core;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Managers;
using Sitecore.TestStar.Core.Utility;

namespace Sitecore.TestStar.TestLauncher.Handlers {
	public class WebConsoleTestHandler : IWebTestHandler {
		#region ITestHandler Events

		public void OnError(TestMethod tm, TestEnvironment te, TestSite ts, TestResult tr, string requestURL, HttpStatusCode responseStatus) {
			WriteMessage(tm, te, ts, "Has Errors", tr.Message);
			Environment.Exit((int)ExitCode.WebTestException);
		}

		public void OnFailure(TestMethod tm, TestEnvironment te, TestSite ts, TestResult tr, string requestURL, HttpStatusCode responseStatus) {
			WriteMessage(tm, te, ts, "Failed", tr.Message);
			Environment.Exit((int)ExitCode.WebTestFailed);
		}

		public void OnSuccess(TestMethod tm, TestEnvironment te, TestSite ts, TestResult tr, string requestURL, HttpStatusCode responseStatus) {
			WriteMessage(tm, te, ts, "Succeeded", string.Empty);
		}

		public void OnSkipped(TestMethod tm, TestEnvironment te, TestSite ts) {
			WriteMessage(null, te, ts, "Skipped", string.Format("{0} doesn't support the {1} environment", ts.Name, te.Name));
		}

		#endregion ITestHandler Events

		private void WriteMessage(TestMethod tm, TestEnvironment te, TestSite ts, string name, string value) {
			Console.WriteLine(string.Format("{0} - {1}", ts.Name, te.Name));
			if (tm != null)
				Console.Write(string.Format("{0} - ", TestUtility.GetClassName(tm.ClassName)));
			Console.WriteLine(string.Format("{0}{1}{2}", name, (value.Length > 0) ? ": " : string.Empty, value));
		}
	}
}
