using Sitecore.TestStar.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.Core.Entities {
	public class TestResultList {

		public string ID = string.Empty;
		public string Title = string.Empty;
		public DateTime Date = DateTime.Now;
        public IEnumerable<ITestResult> ResultEntries;
		
		#region Constructors

		public TestResultList() { }

		public TestResultList(string id, string title, DateTime date, IEnumerable<ITestResult> entries) {
			ID = id;
			Title = title;
			Date = date;
            ResultEntries = entries;
		}

		#endregion Constructors

		public override bool Equals(object obj) {
			return (this.ID.Equals(((TestResultList)obj).ID)) ? true : false;
		}
	}
}
