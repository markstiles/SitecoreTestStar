using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.Core.WebService {
	public class JSONTestResult {

		public bool Flag;
		public string Type;
		public string Method;
		public string Name;
		public string Value;

		public JSONTestResult(bool f, string t, string m, string n, string v){
			Flag = f;
			Type = t;
			Method = m;
			Name = n;
			Value = v;
		}
	}
}
