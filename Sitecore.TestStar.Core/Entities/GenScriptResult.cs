using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.Core.Entities {
	public class GenScriptResult {

		public bool Success;
		public string Message;

		public GenScriptResult() {
			Success = false;
			Message = string.Empty;
		}

		public GenScriptResult(bool success, string message) {
			Success = success;
			Message = message;
		}
	}
}
