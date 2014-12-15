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
	public class UniqueURLTest : BranchingTest {

		public UniqueURLTest() { }
		
		[Test]
		public override void RunTest() {
			SitecoreSite scs = ContextSite.ConvertTo<SitecoreSite>();
			
			if (!string.IsNullOrEmpty(scs.SiteNodeID)) {

				string baseURL = scs.SCBaseURL(ContextEnvironment);

				//create web service
				string wsaddress = string.Format("{0}/{1}", baseURL, "services/test.asmx");

				//create web service
				BasicHttpBinding b = new BasicHttpBinding();
				b.TextEncoding = Encoding.UTF8;
				EndpointAddress a = new EndpointAddress(wsaddress);

				//write the sitemap.xml to publishing targets using web service
				ArrayOfString links = client.GetDistinctUrls(scs.SiteNodeID);

				foreach (string s in links) {
					string url = string.Format("{0}{1}", baseURL, s);
					HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
					try {
						HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
						ResponseStatus = resp.StatusCode;
						resp.Close();
						if (!ResponseStatus.Equals(HttpStatusCode.OK))
							SetFailure(url, string.Format("{0} was {1}", s, ResponseStatus.ToString()));
					} catch (WebException wex) {
						HttpWebResponse resp = (HttpWebResponse)wex.Response;
						ResponseStatus = (resp != null) ? resp.StatusCode : HttpStatusCode.BadRequest;
						SetFailure(url, string.Format("{0} wasn't found. {1}", s, wex.Message));
					}
				}

				if (HasFailed)
					Assert.Fail(Log.ToString());
			}
		}
	}
}
