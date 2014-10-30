using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Core;
using Sitecore.TestStar.Core.Entities;

namespace Sitecore.TestStar.Core.Managers {
	public interface IUnitTestHandler {

		void OnError(TestMethod tm, TestResult tr);

		void OnFailure(TestMethod tm, TestResult tr);

		void OnSuccess(TestMethod tm, TestResult tr);
	}
}
