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
using Sitecore.TestStar.Core.Utility;
using Sitecore.TestStar.UI.Providers;

namespace Sitecore.TestStar.WebTests {
    [TestFixture, RequiresSTA, Category("Sitemap Test")]
	public class SitemapTest : BaseWebTest {

		[Test]
		public void RunTest() {

			string smapPath = string.Format("{0}/{1}", RequestURL, "sitemap.xml");
            
            SCTextEntryProvider t = new SCTextEntryProvider();
            
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
				Assert.Fail(TextProviderPaths.Errors.Webtests.SitemapNotFound(t));
			}

			if (string.IsNullOrEmpty(smap.Trim()))
                Assert.Fail(TextProviderPaths.Errors.Webtests.SitemapEmpty(t));

			XmlDocument xd = new XmlDocument();
			xd.LoadXml(smap);
			XmlNode urlSet = xd.LastChild;
			if (!urlSet.Name.Equals("urlset") || !urlSet.HasChildNodes)
				Assert.Fail(TextProviderPaths.Errors.Webtests.SitemapNoLinks(t));

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
                                LogFailure(child.InnerText, string.Format("{0} {1} {2}", child.InnerText, TextProviderPaths.Errors.Webtests.Was(t), ResponseStatus.ToString()));
						} catch (WebException wex) {
							HttpWebResponse resp = (HttpWebResponse)wex.Response;
							ResponseStatus = (resp != null) ? resp.StatusCode : HttpStatusCode.BadRequest;
							LogFailure(child.InnerText, string.Format("{0} {1} {2}", TextProviderPaths.Errors.Webtests.SitemapLinkNotFound(t), child.InnerText, wex.Message));
						}
					}
				}
			}

			if (HasFailed)
				Assert.Fail(Log.ToString());
		}
	}
}
