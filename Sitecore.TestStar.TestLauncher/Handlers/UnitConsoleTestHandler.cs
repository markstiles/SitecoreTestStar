using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Core;
using Sitecore.TestStar.Core.Managers;
using Sitecore.TestStar.Core.Utility;

namespace Sitecore.TestStar.TestLauncher.Handlers {
	public class UnitConsoleTestHandler : IUnitTestHandler {
		#region ITestHandler Events

		public void OnError(TestMethod tm, TestResult tr) {
			WriteMessage(tm, "Has Errors", tr.Message);
			Environment.Exit((int)ExitCode.UnitTestException);
		}

		public void OnFailure(TestMethod tm, TestResult tr) {
			WriteMessage(tm, "Failed", tr.Message);
			Environment.Exit((int)ExitCode.UnitTestFailed);
		}

		public void OnSuccess(TestMethod tm, TestResult tr) {
			WriteMessage(tm, "Succeeded", string.Empty);
		}

		#endregion ITestHandler Events

		private void WriteMessage(TestMethod tm, string name, string value){
			if (tm != null)
				Console.Write(string.Format("{0} - ", TestUtility.GetClassName(tm.ClassName)));
			Console.WriteLine(string.Format("{0}{1}{2}", name, (value.Length > 0) ? ": " : string.Empty, value));
		}
	}
}
