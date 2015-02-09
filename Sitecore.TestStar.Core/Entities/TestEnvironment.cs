using Sitecore.TestStar.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.Core.Entities {
	public class TestEnvironment : ITestEnvironment {

        #region ITestEnvironment

        protected string _ID;
        public string ID { get { return _ID; } set { _ID = value; } }
        protected string _Name;
        public string Name { get { return _Name; } set { _Name = value; } }
        protected string _DomainPrefix;
        public string DomainPrefix { get { return _DomainPrefix; } set { _DomainPrefix = value; } }
        protected string _IPAddress;
        public string IPAddress { get { return _IPAddress; } set { _IPAddress = value; } }
        
        #endregion ITestEnvironment

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
