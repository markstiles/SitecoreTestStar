using System.Net;
using NUnit.Core;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Extensions;

namespace Sitecore.TestStar.Core.Tests {
	public abstract class BaseWebTest {

		public static readonly string RequestURLKey = "BaseRequestURL";
		public static readonly string ResponseStatusCodeKey = "ResponseStatusCode";
		public static readonly string EnvironmentKey = "ContextEnvironment";
		public static readonly string SiteKey = "ContextSite";

		protected TestMethod CurrentTestMethod;

		protected Test ContextTest {
			get {
				return TestExecutionContext.CurrentContext.CurrentTest;
			}
		}

		protected TestEnvironment ContextEnvironment {
			get {
				return ContextTest.GetProperty<TestEnvironment>(EnvironmentKey);
			}
			set {
				ContextTest.SetProperty(EnvironmentKey, value);
			}
		}

		protected TestSite ContextSite {
			get {
				return ContextTest.GetProperty<TestSite>(SiteKey);
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

		public BaseWebTest() { }

		public abstract void RunTest();
	}
}
