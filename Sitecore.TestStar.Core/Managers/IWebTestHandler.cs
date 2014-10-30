using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NUnit.Core;
using Sitecore.TestStar.Core.Entities;

namespace Sitecore.TestStar.Core.Managers {
	public interface IWebTestHandler {

		void OnError(TestMethod tm, TestEnvironment te, TestSite ts, TestResult tr, string requestURL, HttpStatusCode responseStatus);

		void OnFailure(TestMethod tm, TestEnvironment te, TestSite ts, TestResult tr, string requestURL, HttpStatusCode responseStatus);

		void OnSuccess(TestMethod tm, TestEnvironment te, TestSite ts, TestResult tr, string requestURL, HttpStatusCode responseStatus);

		void OnSkipped(TestMethod tm, TestEnvironment te, TestSite ts);
	}
}
