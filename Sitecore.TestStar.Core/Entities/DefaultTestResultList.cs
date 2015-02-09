using Sitecore.TestStar.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.Core.Entities {
    public class DefaultTestResultList : ITestResultList {

        #region ITestResultList

        protected string _ID;
        public string ID { get { return _ID; } set { _ID = value; } }
        protected string _Title;
        public string Title { get { return _Title; } set { _Title = value; } }
        protected DateTime _Date;
        public DateTime Date { get { return _Date; } set { _Date = value; } }
        protected IEnumerable<ITestResult> _ResultEntries;
        public IEnumerable<ITestResult> ResultEntries { get { return _ResultEntries; } set { _ResultEntries = value; } }

        #endregion ITestResultList

        #region Constructors

        public DefaultTestResultList() { 
            ID = string.Empty;
		    Title = string.Empty;
		    Date = DateTime.Now;
            ResultEntries = new List<ITestResult>();
        }

		public DefaultTestResultList(string id, string title, DateTime date, IEnumerable<ITestResult> entries) {
			ID = id;
			Title = title;
			Date = date;
            ResultEntries = entries;
		}

		#endregion Constructors

		public override bool Equals(object obj) {
			return (this.ID.Equals(((DefaultTestResultList)obj).ID)) ? true : false;
		}
	}
}
