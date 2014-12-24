using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.Core.WebService {
	public class JSONGenScriptResult {

		public bool Success;
		public string Message;

		public JSONGenScriptResult(bool b, string m) {
			Success = b;
			Message = m;
		}
	}
}
