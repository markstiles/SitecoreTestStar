using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Core;
using NUnit.Util;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Extensions;
using Sitecore.TestStar.Core.Providers;
using Sitecore.TestStar.Core.Tests;
using Sitecore.TestStar.Core.Utility;
using Cons = Sitecore.TestStar.Core.Utility.Constants;

namespace Sitecore.TestStar.Core.Managers {
	public class UnitTestManager {

        #region Messaging

        public List<DefaultUnitTestResult> ResultList = new List<DefaultUnitTestResult>();

        #endregion Messaging

		public UnitTestManager() { }

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
				OnResult(tm, tr, TestResultEnum.Error);
			} else if (tr.IsFailure) {
				OnResult(tm, tr, TestResultEnum.Failure);
			} else if (tr.IsSuccess) {
				OnResult(tm, tr, TestResultEnum.Success);
			}
		}

        private void OnResult(TestMethod tm, TestResult tr, TestResultEnum tre) {

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
	}
}
