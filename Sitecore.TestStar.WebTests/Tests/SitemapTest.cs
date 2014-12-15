using System;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
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
	public class SitemapTest : BranchingTest {

		public SitemapTest() { }

		[Test]
		public override void RunTest() {

			string smapPath = string.Format("{0}/{1}", RequestURL, "sitemap.xml");

			//get the sitemap in some non-error throwing way
			string smap = string.Empty;
			try {
				var request = WebRequest.Create(smapPath);
				using (WebResponse response = request.GetResponse()) {
					using (var responseStream = response.GetResponseStream()) {
						TextReader textreader = new StreamReader(responseStream);
						smap = textreader.ReadToEnd();
					}
				}
			} catch (WebException ex) {
				Assert.Fail("There was no sitemap.xml file found.");
			}

			if (string.IsNullOrEmpty(smap.Trim()))
				Assert.Fail("The sitemap.xml was empty.");

			XmlDocument xd = new XmlDocument();
			xd.LoadXml(smap);
			XmlNode urlSet = xd.LastChild;
			if (!urlSet.Name.Equals("urlset") || !urlSet.HasChildNodes)
				Assert.Fail("The sitemap.xml has no links.");

			foreach (XmlNode url in urlSet) {
				if (!url.HasChildNodes)
					continue;
				foreach (XmlNode child in url.ChildNodes) {
					if (child.Name.Equals("loc")) {
						HttpWebRequest req = (HttpWebRequest)WebRequest.Create(child.InnerText);
						try {
							HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
							ResponseStatus = resp.StatusCode;
							resp.Close();
							if (!ResponseStatus.Equals(HttpStatusCode.OK))
								SetFailure(child.InnerText, string.Format("{0} was {1}", child.InnerText, ResponseStatus.ToString()));
						} catch (WebException wex) {
							HttpWebResponse resp = (HttpWebResponse)wex.Response;
							ResponseStatus = (resp != null) ? resp.StatusCode : HttpStatusCode.BadRequest;
							SetFailure(child.InnerText, string.Format("Sitemap link {0} wasn't found. {1}", child.InnerText, wex.Message));
						}
					}
				}
			}

			if (HasFailed)
				Assert.Fail(Log.ToString());
		}
	}
}
