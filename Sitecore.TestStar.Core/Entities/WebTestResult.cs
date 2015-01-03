using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.Core.Entities {
    public class WebTestResult {

		public string ID;
		public DateTime Date;
		public string ClassName;
		public string Method;
		public string Type;
		public string Message;
		public string Site;
		public string Environment;
		public string RequestURL;
		public string ResponseStatus;
		
		public WebTestResult() {
			ID = string.Empty;
			Date = DateTime.Now;
			ClassName = string.Empty;
			Method = string.Empty;
			Type = string.Empty;
			Message = string.Empty;
			Site = string.Empty;
			Environment = string.Empty;
			RequestURL = string.Empty;
			ResponseStatus = string.Empty;
		}

		public WebTestResult(string id, DateTime dateTime, string type, string method, string className, string msg, string site, string env, string url, string status) {
			ID = id;
			Date = dateTime;
			ClassName = className;
			Method = method;
			Type = type;
			Message = (msg == null) ? string.Empty : msg;
			Site = site;
			Environment = env;
			RequestURL = url;
			ResponseStatus = status;
		}
	}
}
