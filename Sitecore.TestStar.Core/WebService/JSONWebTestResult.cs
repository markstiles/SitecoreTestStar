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
		public string Value;
        public string Site;
        public string Environment;

		public JSONWebTestResult(bool f, string t, string m, string n, string v, string s, string e){
			Flag = f;
			Type = t;
			Method = m;
			Name = n;
			Value = v;
            Site = s;
            Environment = e;
		}
	}
}
