using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.Core.WebService {
    public class JSONWebTestResult {

		public bool Flag;
		public string Type;
		public string Method;
		public string Name;
		public string Message;
		public string Site;
		public string Environment;
		public bool Failed;
		
		public JSONWebTestResult() {
			Flag = false;
			Type = string.Empty;
			Method = string.Empty;
			Name = string.Empty;
			Message = string.Empty;
			Site = string.Empty;
			Environment = string.Empty;
			Failed = false;
		}

		public JSONWebTestResult(bool f, string t, string m, string n, string msg, string s, string e, bool failed) {
			Flag = f;
			Type = t;
			Method = m;
			Name = n;
			Message = msg; 
			Site = s;
			Environment = e;
			Failed = failed;
		}
	}
}
