using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.Core.WebService {
	public class JSONUnitTestResult {

		public bool Flag;
		public string Type;
		public string Method;
		public string Name;
		public string Message;
		public bool Failed;
		
		public JSONUnitTestResult() {
			Flag = false;
			Type = string.Empty;
			Method = string.Empty;
			Name = string.Empty;
			Message = string.Empty;
			Failed = false;
		}

		public JSONUnitTestResult(bool f, string t, string m, string n, string msg, bool failed) {
			Flag = f;
			Type = t;
			Method = m;
			Name = n;
			Message = msg;
			Failed = failed;
		}
	}
}
