using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Core;
using Sitecore.TestStar.Core.Utility;

namespace Sitecore.TestStar.Core.Managers {
	public interface IUnitTestHandler {
		void OnResult(TestMethod tm, TestResult tr, TestResultEnum tre);
	}
}
