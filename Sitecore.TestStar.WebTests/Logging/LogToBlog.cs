using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.WebTests.Logging {
	public class LogToBlog {
	
		public static void LogResults(string testName, string url, HttpStatusCode statusCode, bool showBodyText, string message) {

			StringBuilder fullText = new StringBuilder();
			StringBuilder descText = new StringBuilder();

			//short text
			descText.AppendFormat("<div class='resultLink'>{0}: {1}</div>", GetKeyStr(((int)statusCode).ToString()), MakeHref(url));

			//full text
			fullText.Append("<div class='resultError'>");
			fullText.AppendFormat("{0}: {1}<br/>", GetKeyStr("Time"), DateTime.Now.ToString("h:mm tt"));
			fullText.AppendFormat("{0}: {1}<br/>", GetKeyStr("URL"), MakeHref(url));
			fullText.AppendFormat("{0}: {1} - {2}<br/>", GetKeyStr("Status Code"), ((int)statusCode).ToString(), statusCode.ToString());
			if (showBodyText)
				fullText.AppendFormat("{0}: {1}<br/>", GetKeyStr("Body"), message);
			fullText.Append("</div>");

			//if there are error then create a blog post
			if (fullText.Length > 0) {

				//create web service parts
				BasicHttpBinding b = new BasicHttpBinding();
				b.TextEncoding = Encoding.UTF8;

				//TEST WEB SERVICE
				//EndpointAddress end1 = new EndpointAddress(service1);
				
				//gwsClient.PostTestResults("{4BC28612-D725-4348-ACDB-C102E8664FF5}", DateTime.Now, "NUnit LogToBlog", testName, descText.ToString(), fullText.ToString(), new ArrayOfString(), testName);
			}
		}

		protected static string MakeHref(string a) {
			return string.Format("<a href='{0}' target='_blank'>{0}</a>", a);
		}

		protected static string GetKeyStr(string s) {
			return string.Format("<span class='resultKey'>{0}</span>", s);
		}
	}
}
