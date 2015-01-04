using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NUnit.Core;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Tests;
using Sitecore.TestStar.Core.Utility;
using Sitecore.TestStar.Core.Tests;

namespace Sitecore.TestStar.WebTests.Tests {
	/// <summary>
	/// Branching indicates that the test itself handles more than one single request per instance created. This is due to testing multiple web services etc.
	/// </summary>
	public abstract class BranchingTest : BaseWebTest {

		protected bool HasFailed = false;
		protected StringBuilder Log = new StringBuilder();

		public BranchingTest() { }
		
		protected void SetFailure(string requestURL, string message) {
			HasFailed = true;
			Log.Append(message).AppendLine();
		}
	}
}
