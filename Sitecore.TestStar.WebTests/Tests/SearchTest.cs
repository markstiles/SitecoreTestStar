using System;
using System;
using System.Net;
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
	public class SearchTest : BaseWebTest {
	
		public SearchTest() { }
		
		[Test]
		public override void RunTest() {

			SitecoreSite scs = ContextSite.ConvertTo<SitecoreSite>();
			if (scs.SearchPageExists) {
				//build base url
				string baseURL = (string.IsNullOrEmpty(scs.LanguageCode))
					? scs.BaseURL(ContextEnvironment)
					: string.Format("{0}/{1}", ContextSite.BaseURL(ContextEnvironment), scs.LanguageCode);
				//build the search path
				string SearchPagePath = (string.IsNullOrEmpty(scs.SearchPagePath))
					? "search-results.aspx"
					: scs.SearchPagePath;
				//build the criteria
				string SearchPageCriteria = "criteria=sdfg&page=1";
				string url = string.Format("{0}/{1}?{2}", baseURL, SearchPagePath, SearchPageCriteria);

				HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
				req.AllowAutoRedirect = false;
				try {
					HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
					ResponseStatus = resp.StatusCode;
					resp.Close();
					if (!(((int)ResponseStatus).Equals(200)))
						Assert.Fail("Search page wasn't found");
					//could add watin test for search form
				} catch (WebException wex) {
					HttpWebResponse resp = (HttpWebResponse)wex.Response;
					ResponseStatus = (resp != null) ? resp.StatusCode : HttpStatusCode.BadRequest;
					Assert.Fail(wex.Message);
				}
			}
		}
	}
}
