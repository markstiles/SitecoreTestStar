using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Core;
using NUnit.Util;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Tests;
using Sitecore.TestStar.Core.Utility;

namespace Sitecore.TestStar.Core.Managers {
	public class UnitTestManager {

		private IUnitTestHandler Handler;

		public UnitTestManager(IUnitTestHandler handler) {
			if (handler == null)
				throw new ArgumentNullException("The handler provided is null");
			Handler = handler;
		}

		public void RunTest(TestMethod tm) {
			if (tm == null)
				throw new NullReferenceException("Test Method was null. Make sure the class method has the [Test] attribute.");

			var t = new Thread(new ThreadStart(() => HandleTest(tm)));
			t.SetApartmentState(ApartmentState.STA);
			t.Start();
			t.Join();
		}

		/// <summary>
		/// deals with the results of tests
		/// </summary>
		private void HandleTest(TestMethod tm) {

			TestResult tr = tm.Run(new NullListener(), TestFilter.Empty);
			ResultSummarizer summ = new ResultSummarizer(tr);

			if (tr.IsError) {
				Handler.OnResult(tm, tr, TestResultEnum.Error);
			} else if (tr.IsFailure) {
				Handler.OnResult(tm, tr, TestResultEnum.Failure);
			} else if (tr.IsSuccess) {
				Handler.OnResult(tm, tr, TestResultEnum.Success);
			}
		}
	}
}
