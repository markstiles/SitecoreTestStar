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
using Sitecore.TestStar.Core.Providers;

namespace Sitecore.TestStar.WebTests {
    [TestFixture, RequiresSTA, Category("Redirect Test")]
	public class RedirectTest : BaseWebTest {

		[Test]
		public void RunTest() {
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(RequestURL);
			req.AllowAutoRedirect = false;
			try {
				HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
				ResponseStatus = resp.StatusCode;
				resp.Close();
                SCTextEntryProvider t = new SCTextEntryProvider();
                Assert.IsTrue((((int)ResponseStatus).Equals(301) || ((int)ResponseStatus).Equals(302)), string.Format("{0}: {1}", TextProviderPaths.Errors.Webtests.NotRedirect(t), ((int)ResponseStatus).ToString()));
			} catch (WebException wex) {
				HttpWebResponse resp = (HttpWebResponse)wex.Response;
				ResponseStatus = (resp != null) ? resp.StatusCode : HttpStatusCode.BadRequest;
				Assert.Fail(wex.Message);
			}
		}
	}
}
