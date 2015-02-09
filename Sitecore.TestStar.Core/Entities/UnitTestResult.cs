using Sitecore.TestStar.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.Core.Entities {
	public class UnitTestResult : ITestResult {

        #region ITestResult

        protected string _ID;
        public string ID { get { return _ID; } set { _ID = value; } }
        protected DateTime _Date;
        public DateTime Date { get { return _Date; } set { _Date = value; } }
        protected string _ClassName;
        public string ClassName { get { return _ClassName; } set { _ClassName = value; } }
        protected string _Method;
        public string Method { get { return _Method; } set { _Method = value; } }
        protected string _Type;
        public string Type { get { return _Type; } set { _Type = value; } }
        protected string _Message;
        public string Message { get { return _Message; } set { _Message = value; } }
        protected string _AdditionalInfo;
        public string AdditionalInfo { get { return _AdditionalInfo; } set { _AdditionalInfo = value; } }

        #endregion ITestResult
        
		public UnitTestResult() {
			ID = string.Empty;
			Date = DateTime.Now;
			ClassName = string.Empty;
			Method = string.Empty;
			Type = string.Empty;
			Message = string.Empty;
            AdditionalInfo = string.Empty;
		}

		public UnitTestResult(string id, DateTime dateTime, string type, string method, string className, string msg) {
			ID = id;
			Date = dateTime;
			ClassName = className;
			Method = method;
			Type = type;
			Message = (msg == null) ? string.Empty : msg;
            AdditionalInfo = string.Empty;
		}
    }
}
