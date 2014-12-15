using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.ServiceModel;
using System.Text;
using NUnit.Core;
using NUnit.Framework;
using NUnit.Util;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Extensions;
using Sitecore.TestStar.Core.Tests;
using Sitecore.TestStar.WebTests.Entities;
using Sitecore.TestStar.WebTests.Logging;

namespace Sitecore.TestStar.WebTests.Tests {
	[TestFixture, RequiresSTA]
	public class IPTest : BaseWebTest {

		public IPTest() { }

		[Test]
		public override void RunTest() {

			//try {
				IEnumerable<IPAddress> ips = Dns.GetHostAddresses(ContextSite.Domain).Where(a => a.AddressFamily == AddressFamily.InterNetwork);
				if (ips != null && ips.Any()) {
					if (!ips.Where(a => a.ToString().Equals(ContextEnvironment.IPAddress)).Any()) {
						StringBuilder sb = new StringBuilder();
						sb.AppendFormat("Expected: {0}<br/>", ContextEnvironment.IPAddress);
						sb.Append("Actual: ");
						StringBuilder sbip = new StringBuilder();
						foreach (IPAddress ip in ips)
							sbip.AppendFormat("[{0}] ", ip.ToString());
						sb.Append(sbip.ToString());
						string errUrl = string.Format("{0}-expected:{1}-actual:{2}", RequestURL, ContextEnvironment.IPAddress, sbip.ToString());
						Assert.Fail(errUrl, sb.ToString());
					}
				}
			//} catch (WebException wex) {
			//	HttpWebResponse resp = (HttpWebResponse)wex.Response;
			//	ResponseStatus = (resp != null) ? resp.StatusCode : HttpStatusCode.BadRequest;
			//	Assert.Fail(wex.Message);
			//}
		}
	}
}
