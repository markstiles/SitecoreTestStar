using System;
using System.Linq;
using System.Collections.Specialized;
using System.Data;
using System.Reflection;
using NUnit.Core;
using NUnit.Core.Filters;
using Sitecore.TestStar.Core.Utility;
using Sitecore.TestStar.Core.Extensions;
using Sitecore.TestStar.Core.Managers;
using System.Web.UI;
using Sitecore.TestStar.Core.Entities;
using System.Text;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.IO;
using Cons = Sitecore.TestStar.Core.Utility.Constants;
using Sitecore.TestStar.Core.Providers;

namespace Sitecore.TestStar.Core.UI.sublayouts {
	public partial class UnitTestPage : UserControl, IUnitTestHandler {

		#region Properties

		private List<TestFixture> _Fixtures;
		protected List<TestFixture> Fixtures {
			get {
				if (_Fixtures == null)
					_Fixtures = new List<TestFixture>();
				return _Fixtures;
			}
			set {
				_Fixtures = value;
			}
		}

		TestSuite suite = null;

		#endregion Properties

		#region Events

		protected void Page_Load(object sender, EventArgs e) {

			// Initialise NUnit
			CoreExtensions.Host.InitializeService();
			// Find tests in current assembly
			suite = TestUtility.GetTestSuite(AssemblyProvider.GetUnitTestAssemblies().First());// Cons.DefaultUnitTestAssembly);

			//get dictionaries for forms and querying
			foreach (TestFixture tf in suite.GetFixtures())
				Fixtures.Add(tf);

			// Initialise controls
			ltlResults.Text = string.Empty;
			ltlError.Text = string.Empty;
			ltlLog.Text = string.Empty;

			if (!IsPostBack) {
				// Display category filters
				cblCategories.DataSource = suite.GetAllCategories().OrderBy(a => a);
				cblCategories.DataBind();
			}
		}

		protected void btnSubmitTests_Click(object sender, EventArgs e) {

			UnitTestManager manager = new UnitTestManager(this);
			
			IEnumerable<ListItem> selectedCats = cblCategories.Items.Cast<ListItem>().Where(a => a.Selected);
			
			//build dictionary with any methods with the selected categories
			Dictionary<string, TestMethod> sets = new Dictionary<string, TestMethod>();
			if (selectedCats.Any()) {
				foreach (ListItem li in selectedCats) {
					foreach (TestFixture tf in Fixtures) {
						bool fixtHasCat = (tf.Categories().Any(b => b.Equals(li.Value)));
						foreach (TestMethod tm in tf.Tests) {
							//don't add twice and if fixture or the method has the selected category then add
							if (!sets.ContainsKey(tm.MethodName) && (fixtHasCat || tm.Categories().Any(b => b.Equals(li.Value))))
								sets.Add(tm.MethodName, tm);
						}
					}
				}
			} else { // add all
				sets = suite.GetMethods().ToDictionary(a => a.MethodName);
			}

			//run all tests found
			foreach (TestMethod method in sets.Values)
				manager.RunTest(method);
		}

		/// <summary>
		/// Generates a batch script from the configured test on the page
		/// </summary>
		protected void btnCreateScript_Click(object sender, EventArgs e) {
			string scriptName = txtScriptName.Text;
			if (string.IsNullOrEmpty(scriptName)) {
				LogError("Script Generator", "Need to provide a script name");
				return;
			}

			StringBuilder sb = new StringBuilder();
			sb.Append("@echo off").AppendLine();
			sb.AppendFormat(@"set TestLauncherPath=%0\..\..\bin\{0}.exe", Cons.DefaultTestLauncher).AppendLine().AppendLine();
			sb.AppendLine("@echo on");

			StringBuilder cats = new StringBuilder();
			foreach (ListItem li in cblCategories.Items.Cast<ListItem>().Where(a => a.Selected)) {
				if (cats.Length > 0) 
					cats.Append(",");
				cats.Append(li.Value);
			}

			//define exe, assembly, categories and name(blank)
			//sb.AppendFormat("\"%TestLauncherPath%\" \"-u\" \"{0}\" \"{1}\" \"\"", Cons.DefaultUnitTestAssembly, cats.ToString());
			sb.AppendLine().AppendLine("pause");

			//write file
			string filePath = string.Format(@"{0}/sitecore modules/web/TestStar/scripts/{1}.bat", Cons.ApplicationRoot, scriptName);
			using (StreamWriter newData = new StreamWriter(filePath, false)) {
				newData.WriteLine(sb.ToString());
			}
			Log("Script Generator", string.Format("Successfully Created {0}", filePath));
		}

		#endregion Events

		#region ITestHandler Events

		public void OnError(TestMethod tm, TestResult tr) {
			Results(tm, "Has Errors", tr.Message, "err");
		}

		public void OnFailure(TestMethod tm, TestResult tr) {
			Results(tm, "Failed", tr.Message, "fail");
		}

		public void OnSuccess(TestMethod tm, TestResult tr) {
			Results(tm, "Succeeded", string.Empty, "pass");
		}

		#endregion ITestHandler Events

		#region UI Messaging

		private bool ResultFlag = false;

		/// <summary>
		/// writes message to the results window
		/// </summary>
		protected void Results(TestMethod tm, string name, string value, string type) {
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("<div class='result {0} {1}'>", (ResultFlag) ? "even" : "odd", type).AppendLine();
			if (tm != null) {
				sb.AppendFormat("<div class='rMethod'>{0} -</div>", TestUtility.GetClassName(tm.MethodName)).AppendLine();
			}
			sb.AppendFormat("<div class='rResult'>{0}{1}{2}</div>", name, (value.Length > 0) ? ": " : string.Empty, value).AppendLine();
			sb.Append("</div>").AppendLine();
			ltlResults.Text += sb.ToString();

			ResultFlag = !ResultFlag;
		}

		/// <summary>
		/// writes message to the log window
		/// </summary>
		protected void Log(string name, string value) {
			ltlLog.Text += string.Format("{0}: {1}<br/>", name, value);
		}

		protected void LogError(string name, string value) {
			ltlError.Text += string.Format("{0}: {1}<br/>", name, value);
		}

		#endregion UI Messaging
	}
}
