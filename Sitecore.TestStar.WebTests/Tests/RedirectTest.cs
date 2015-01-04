using System;
using System.Net;
using System.Text;
using NUnit.Core;
using NUnit.Framework;
using NUnit.Util;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Extensions;
using Sitecore.TestStar.Core.Tests;
using Sitecore.TestStar.Core.Utility;

namespace Sitecore.TestStar.WebTests.Tests {
	[TestFixture, RequiresSTA]
	public class RedirectTest : BaseWebTest {

		public RedirectTest() { }
		
		[Test]
		public override void RunTest() {
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(RequestURL);
			req.AllowAutoRedirect = false;
			try {
				HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
				ResponseStatus = resp.StatusCode;
				resp.Close();
				Assert.IsTrue((((int)ResponseStatus).Equals(301) || ((int)ResponseStatus).Equals(302)), string.Format("HttpStatusCode should be 301 or 302 but was: {0}", ((int)ResponseStatus).ToString()));
			} catch (WebException wex) {
				HttpWebResponse resp = (HttpWebResponse)wex.Response;
				ResponseStatus = (resp != null) ? resp.StatusCode : HttpStatusCode.BadRequest;
				Assert.Fail(wex.Message);
			}
		}
	}
}
