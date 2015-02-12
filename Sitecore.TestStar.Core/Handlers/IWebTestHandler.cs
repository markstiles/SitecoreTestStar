using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NUnit.Core;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Utility;
using Sitecore.TestStar.Core.Entities.Interfaces;

namespace Sitecore.TestStar.Core.Handlers {
	public interface IWebTestHandler {

		void OnResult(TestMethod tm, ITestEnvironment te, ITestSite ts, TestResult tr, string requestURL, HttpStatusCode responseStatus, TestResultEnum tre);
	}
}
