using System.Net;
using System.Text;
using System.Linq;
using NUnit.Core;
using NUnit.Framework;
using NUnit.Util;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Extensions;
using Sitecore.TestStar.Core.Utility;
using Sitecore.TestStar.Core.Tests;
using Sitecore.TestStar.WebTests.Logging;
using System.Collections.Generic;
using System.Threading;
using System;
using Sitecore.TestStar.Core.Providers;

namespace Sitecore.TestStar.WebTests.Tests {
	[TestFixture, RequiresSTA]
	public class PingTest : BaseWebTest {

		public PingTest() { }
		
		[Test]
		public override void RunTest() {
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(RequestURL);
			try { // catches the 400 and 500 errors by exception
				HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
				HttpStatusCode sc = resp.StatusCode;
				//store it to pass it out
				ResponseStatus = resp.StatusCode;
				resp.Close();
				Assert.AreEqual(HttpStatusCode.OK, sc);
			} catch (WebException wex) {
				HttpWebResponse resp = (HttpWebResponse)wex.Response;
				ResponseStatus = (resp != null) ? resp.StatusCode : HttpStatusCode.BadRequest;
				Assert.Fail(wex.Message);
			}
		}
	}
}
