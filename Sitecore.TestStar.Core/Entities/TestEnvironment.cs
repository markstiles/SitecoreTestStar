using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.Core.Entities {
	public class TestEnvironment {

		public string ID = string.Empty;
		public string Name = string.Empty;
		public string DomainPrefix = string.Empty;
		public string IPAddress = string.Empty;

		#region Constructors

		public TestEnvironment() { }

		public TestEnvironment(string id, string name, string domainPrefix, string ipAddress) {
			ID = id;
			Name = name;
			DomainPrefix = domainPrefix;
			IPAddress = ipAddress;
		}

		#endregion Constructors

		public override bool Equals(object obj) {
			return (this.ID.Equals(((TestEnvironment)obj).ID)) ? true : false;
		}
	}
}
