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
using Sitecore.TestStar.Core.Providers;
using Sitecore.TestStar.UI.Providers;
using Sitecore.TestStar.Core.Utility;

namespace Sitecore.TestStar.WebTests {
	[TestFixture, RequiresSTA, Category("IP Test")]
	public class IPTest : BaseWebTest {
        
		[Test]
		public void RunTest() {

			IEnumerable<IPAddress> ips = Dns.GetHostAddresses(ContextSite.Domain).Where(a => a.AddressFamily == AddressFamily.InterNetwork);
			if (ips != null && ips.Any()) {
				if (!ips.Where(a => a.ToString().Equals(ContextEnvironment.IPAddress)).Any()) {
                    SCTextEntryProvider t = new SCTextEntryProvider();

                    StringBuilder sb = new StringBuilder();
					sb.AppendFormat("{0}: {1}<br/>", TextProviderPaths.Errors.Webtests.Expected(t), ContextEnvironment.IPAddress);
                    sb.AppendFormat("{0}: ", TextProviderPaths.Errors.Webtests.Actual(t));
					StringBuilder sbip = new StringBuilder();
					foreach (IPAddress ip in ips)
						sbip.AppendFormat("[{0}] ", ip.ToString());
					sb.Append(sbip.ToString());
					string errUrl = string.Format("{0}-{1}:{2}-{3}:{4}", RequestURL, TextProviderPaths.Errors.Webtests.Expected(t), ContextEnvironment.IPAddress, TextProviderPaths.Errors.Webtests.Actual(t), sbip.ToString());
					Assert.Fail(errUrl, sb.ToString());
				}
			}
		}
	}
}
