using System.Collections.Generic;
using System.Net;
using System.ServiceModel;
using System.Text;
using NUnit.Core;
using NUnit.Framework;
using NUnit.Util;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Extensions;
using Sitecore.TestStar.Core.Tests;
using Sitecore.TestStar.WebTests.Logging;
namespace Sitecore.TestStar.WebTests.Tests {
	[TestFixture, RequiresSTA]
	public class PlatformTest : BranchingTest {

		private static bool TestEmailers = false;

		private static List<string> GetPaths() {
			List<string> paths = new List<string>();

			//pages of note
			//paths.Add("FormDatabase.aspx");
			//paths.Add("FormUsageViewer.aspx");
			//paths.Add("CaptchaImage.aspx");
			//handlers
			paths.Add("sitemap.xml");
			paths.Add("robots.txt");
			paths.Add("SurgeonViewCSV.ashx");
			paths.Add("layouts/ExternalStringData.ashx");
			paths.Add("BrightcoveVideo.ashx?player=792857234001&video=826063379001&autoStart=true");
			paths.Add("layouts/ExternalStringData.ashx");

			return paths;
		}

		public PlatformTest() { }
		
		[Test]
		public override void RunTest() {

			foreach (string s in GetPaths()) {
				string url = string.Format("{0}/{1}", RequestURL, s);
				HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
				try {
					HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
					ResponseStatus = resp.StatusCode;
					resp.Close();
					if (!ResponseStatus.Equals(HttpStatusCode.OK))
						SetFailure(url, string.Format("{0} returned {1}", url, ResponseStatus.ToString()));
				} catch (WebException wex) {
					HttpWebResponse resp = (HttpWebResponse)wex.Response;
					ResponseStatus = (resp != null) ? resp.StatusCode : HttpStatusCode.BadRequest;
					SetFailure(url, string.Format("{0} - {1}", url, wex.Message));
				}
			}

			//create web service parts
			BasicHttpBinding b = new BasicHttpBinding();
			b.TextEncoding = Encoding.UTF8;

			//TEST WEB SERVICE
			//EndpointAddress end1 = new EndpointAddress(service1);
			//ArrayOfString links = gwsClient.GetDistinctUrls(scs.SiteNodeID);
			//if (links.Count < 1)
				//SetFailure(service1, string.Format("{0} provided no results.", service1)); 
			
			if (HasFailed)
				Assert.Fail(Log.ToString());
		}
	}
}
