using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.Core.Entities.Interfaces {
    public interface ITestSite {

        string ID { get; set; }
		string Name { get; set; }
		string Domain { get; set; }
		string SystemID { get; set; }
		bool Disabled { get; set; }
		string LanguageCode { get; set; }
		string SiteNodeID { get; set; }

		Dictionary<string, object> Properties { get; set; }
		IEnumerable<ITestEnvironment> Environments { get; set; }

        string BaseURL(ITestEnvironment env);
    }
}
