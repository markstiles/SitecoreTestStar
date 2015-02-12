using NUnit.Core;
using NUnit.Util;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Extensions;
using Sitecore.TestStar.Core.Providers;
using Sitecore.TestStar.Core.Providers.Interfaces;
using Sitecore.TestStar.Core.Utility;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Sitecore.TestStar.Core.Managers {
	public class UnitTestManager {

        #region Messaging

        public List<DefaultUnitTestResult> ResultList = new List<DefaultUnitTestResult>();

        #endregion Messaging

        ITextEntryProvider TextProvider;

        public UnitTestManager(ITextEntryProvider t) {
            if (t == null)
                throw new NullReferenceException();
            TextProvider = t;
        }

		public void RunTest(TestMethod tm) {
			if (tm == null)
                throw new NullReferenceException(TextProvider.GetTextByKey("/Exceptions/Managers/TestMethodNull"));

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
                tm.FixtureType.FullName,
                string.Empty,
                DateTime.Now,
                tre.ToString(),
                TestUtility.GetClassName(tm.MethodName),
                TestUtility.GetClassName(((Test)tm).ClassName),
                tr.Message
            );

            ResultList.Add(utr);
        }
	}
}
