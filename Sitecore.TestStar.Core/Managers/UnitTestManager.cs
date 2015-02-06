using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Core;
using NUnit.Util;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Providers;
using Sitecore.TestStar.Core.Tests;
using Sitecore.TestStar.Core.Utility;
using Cons = Sitecore.TestStar.Core.Utility.Constants;

namespace Sitecore.TestStar.Core.Managers {
	public class UnitTestManager {

		private IUnitTestHandler Handler;

		public UnitTestManager(IUnitTestHandler handler) {
			if (handler == null)
				throw new ArgumentNullException(SCTextEntryProvider.Exceptions.Managers.IUnitTestHandlerNull);
			Handler = handler;
		}

		public void RunTest(TestMethod tm) {
			if (tm == null)
				throw new NullReferenceException(SCTextEntryProvider.Exceptions.Managers.TestMethodNull);

			var t = new Thread(new ThreadStart(() => HandleTest(tm)));
			t.SetApartmentState(ApartmentState.STA);
			t.Start();
			t.Join();
		}

		/// <summary>
		/// Passes the proper test result to the handler method
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
