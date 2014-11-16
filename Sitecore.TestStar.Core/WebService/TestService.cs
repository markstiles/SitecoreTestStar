using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services;
using NUnit.Core;
using Sitecore.Sites;
using Sitecore.TestStar.Core.Utility;
using Sitecore.TestStar.Core.Extensions;

namespace Sitecore.TestStar.WebService {
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	[System.Web.Script.Services.ScriptService]
	public class TestService : System.Web.Services.WebService {
		[WebMethod]
		public List<string> GetCategories(string TestSuiteName) {
			CoreExtensions.Host.InitializeService();
			TestSuite t = TestUtility.GetTestSuite(TestSuiteName);
			return t.GetAllCategories().OrderBy(a => a).ToList();
		}
	}
}
