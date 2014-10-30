using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SHDocVw;
using WatiN.Core;

namespace Sitecore.TestStar.Core.Entities {
	public class NavigationObserver {
		private HttpStatusCode _statusCode;

		public NavigationObserver(IE ie) {
			InternetExplorer exp = (InternetExplorer)ie.InternetExplorer;
			exp.NavigateError += new DWebBrowserEvents2_NavigateErrorEventHandler(IeNavigateError);
		}

		public void ShouldHave(HttpStatusCode expectedStatusCode) {
			if (!_statusCode.Equals(expectedStatusCode)) {
				HttpStatusCode sc = (HttpStatusCode)_statusCode;
				string sc1 = Enum.GetName(typeof(HttpStatusCode), sc);
				Assert.Fail(string.Format(CultureInfo.InvariantCulture, "Wrong status code. Expected {0}, but was {1}",
					expectedStatusCode, sc.ToString()));
			}
		}

		private void IeNavigateError(object pDisp, ref object URL, ref object Frame, ref object StatusCode, ref bool Cancel) {
			_statusCode = (HttpStatusCode)StatusCode;
		}
	}
}
