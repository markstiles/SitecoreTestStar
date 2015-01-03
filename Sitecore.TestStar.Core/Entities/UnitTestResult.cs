using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.Core.Entities {
	public class UnitTestResult {

		public string ID;
		public string Type;
		public string Method;
		public string ClassName;
		public string Message;
		
		public UnitTestResult() {
			ID = string.Empty;
			Type = string.Empty;
			Method = string.Empty;
			ClassName = string.Empty;
			Message = string.Empty;
		}

		public UnitTestResult(string id, string type, string method, string className, string msg) {
			ID = id;
			Type = type;
			Method = method;
			ClassName = className;
			Message = (msg == null) ? string.Empty : msg;
		}
	}
}
