using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.Core.Entities {
	public class UnitTestResult {

		public string ID;
		public DateTime Date;
		public string ClassName;
		public string Method;
		public string Type;
		public string Message;
		
		public UnitTestResult() {
			ID = string.Empty;
			Date = DateTime.Now;
			ClassName = string.Empty;
			Method = string.Empty;
			Type = string.Empty;
			Message = string.Empty;
		}

		public UnitTestResult(string id, DateTime dateTime, string type, string method, string className, string msg) {
			ID = id;
			Date = dateTime;
			ClassName = className;
			Method = method;
			Type = type;
			Message = (msg == null) ? string.Empty : msg;
		}
	}
}
