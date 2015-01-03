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
		
		#region Constructors

		public TestResultList() { }

		public TestResultList(string id, string title, DateTime date) {
			ID = id;
			Title = title;
			Date = date;
		}

		#endregion Constructors

		public override bool Equals(object obj) {
			return (this.ID.Equals(((TestResultList)obj).ID)) ? true : false;
		}
	}
}
