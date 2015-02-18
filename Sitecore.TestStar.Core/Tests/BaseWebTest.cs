using System.Net;
using NUnit.Core;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Extensions;
using Sitecore.TestStar.Core.Entities.Interfaces;
using System.Text;

namespace Sitecore.TestStar.Core.Tests {
	public abstract class BaseWebTest {

		public static readonly string RequestURLKey = "BaseRequestURL";
		public static readonly string ResponseStatusCodeKey = "ResponseStatusCode";
		public static readonly string EnvironmentKey = "ContextEnvironment";
		public static readonly string SiteKey = "ContextSite";

        protected bool HasFailed = false;
        protected StringBuilder Log = new StringBuilder();

		protected TestMethod CurrentTestMethod;

		protected Test ContextTest {
			get {
				return TestExecutionContext.CurrentContext.CurrentTest;
			}
		}

		protected ITestEnvironment ContextEnvironment {
			get {
				return ContextTest.GetProperty<ITestEnvironment>(EnvironmentKey);
			}
			set {
				ContextTest.SetProperty(EnvironmentKey, value);
			}
		}

		protected ITestSite ContextSite {
			get {
				return ContextTest.GetProperty<ITestSite>(SiteKey);
			}
			set {
				ContextTest.SetProperty(SiteKey, value);
			}
		}

		protected HttpStatusCode ResponseStatus {
			get {
				return ContextTest.GetProperty<HttpStatusCode>(ResponseStatusCodeKey);
			}
			set {
				ContextTest.SetProperty(ResponseStatusCodeKey, value);
			}
		}

		protected string RequestURL {
			get {
				return ContextTest.GetProperty<string>(RequestURLKey);
			}
			set {
				ContextTest.SetProperty(RequestURLKey, value);
			}
		}
        
        protected void LogFailure(string requestURL, string message) {
            HasFailed = true;
            Log.Append(message).AppendLine();
        }
	}
}
