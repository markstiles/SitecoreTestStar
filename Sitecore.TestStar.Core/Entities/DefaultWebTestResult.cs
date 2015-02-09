using Sitecore.TestStar.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.Core.Entities {
    public class DefaultWebTestResult : ITestResult {

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
        public string AdditionalInfo { 
            get {
                return string.Format("{0} - {1}: {2}<br/>{3}", Site, Environment, ResponseStatus, RequestURL);
            } 
            set { /*_AdditionalInfo = value; */ } 
        }

        #endregion ITestResult

        public string Site;
		public string Environment;
		public string RequestURL;
		public string ResponseStatus;
		
		public DefaultWebTestResult() {
			ID = string.Empty;
			Date = DateTime.Now;
			ClassName = string.Empty;
			Method = string.Empty;
			Type = string.Empty;
			Message = string.Empty;
			Site = string.Empty;
			Environment = string.Empty;
			RequestURL = string.Empty;
			ResponseStatus = string.Empty;
		}

		public DefaultWebTestResult(string id, DateTime dateTime, string type, string method, string className, string msg, string site, string env, string url, string status) {
			ID = id;
			Date = dateTime;
			ClassName = className;
			Method = method;
			Type = type;
			Message = (msg == null) ? string.Empty : msg;
			Site = site;
			Environment = env;
			RequestURL = url;
			ResponseStatus = status;
		}
	}
}
