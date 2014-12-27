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
		public string Value;

		public JSONUnitTestResult() {
			Flag = false;
			Type = string.Empty;
			Method = string.Empty;
			Name = string.Empty;
			Value = string.Empty;
		}

		public JSONUnitTestResult(bool f, string t, string m, string n, string v){
			Flag = f;
			Type = t;
			Method = m;
			Name = n;
			Value = v;
		}
	}
}
