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
	public class RebuildLucene : BaseWebTest {

		private static List<string> GetIndexes() {
			List<string> indexes = new List<string>();

			//indexes of note
			indexes.Add("GlossaryPageIndex");
			indexes.Add("BlogIndex");

			return indexes;
		}
		
		private StringBuilder Log = new StringBuilder();

		public RebuildLucene() { }
		
		[Test]
		public override void RunTest() {
			
			bool HasFailed = false;
			
			List<string> indexes = GetIndexes();
			foreach (string s in indexes) {

				//create web service parts
				BasicHttpBinding b = new BasicHttpBinding();
				b.TextEncoding = Encoding.UTF8;

				//TEST WEB SERVICE
				//EndpointAddress end1 = new EndpointAddress(url);
				//bool success = gwsClient.RebuildLuceneIndex(s);
				//if (!success) {
					//SitecoreSite scs = ContextSite.ConvertTo<SitecoreSite>();
					//HasFailed = true;
					//Log.AppendFormat("{0} failed to rebuild:{1}", url, s).AppendLine();
				//}
			}

			if(HasFailed)
				Assert.Fail(Log.ToString());
		}
	}
}
