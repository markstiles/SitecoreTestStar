using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services;
using NUnit.Core;
using Sitecore.Sites;
using Sitecore.TestStar.Core.Utility;
using Sitecore.TestStar.Core.Extensions;
using Sitecore.TestStar.Core.Managers;
using Sitecore.TestStar.Core.Providers;
using Sitecore.TestStar.Core.Entities;
using Cons = Sitecore.TestStar.Core.Utility.Constants;
using Sitecore.Data.Items;
using System.IO;
using System.Web;
using Sitecore.TestStar.Core.Providers.Interfaces;
using Sitecore.TestStar.Core.Entities.Interfaces;

namespace Sitecore.TestStar.WebService {
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	[System.Web.Script.Services.ScriptService]
	public class TestService : System.Web.Services.WebService {

		[WebMethod]
		public List<DefaultUnitTestResult> RunUnitTests(string AssemblyName, string Category) {
			
			//warm up nunit and configure the manager and handler
			CoreExtensions.Host.InitializeService();
			UnitTestManager manager = new UnitTestManager(new SCTextEntryProvider());

			//build dictionary with any methods with the selected categories
			IEnumerable<TestMethod> methods = TestUtility.GetTestSuite(AssemblyName)
                .GetFixtures()
                .SelectMany<TestFixture, TestMethod>(tf => tf.Tests.Cast<TestMethod>()
                    .Where(tm =>
                        tf.Categories().Any(c => c.Equals(Category)) // if category is on the TestFixture
                        ||
                        tm.Categories().Any(c => c.Equals(Category)) // if category is on the Test method
                    )
                 )
                .Distinct();
            
			//run all tests found
            foreach (TestMethod m in methods)
				manager.RunTest(m);

            List<DefaultUnitTestResult> rl = manager.ResultList;
            foreach(DefaultUnitTestResult d in rl)
                d.ID = SitecoreUtility.CreateResultEntry(d);
            
			return rl;
		}

        [WebMethod]
        public List<DefaultWebTestResult> RunWebTest(string EnvironmentID, string SiteID, string AssemblyName, string ClassName) {

            SCTextEntryProvider tProvider = new SCTextEntryProvider();
            
			//warm up nunit and configure the manager and handler
            CoreExtensions.Host.InitializeService();
            WebTestManager manager = new WebTestManager(new SCTextEntryProvider());

			//build an error list to pass if you need it
			List<DefaultWebTestResult> errorList = new List<DefaultWebTestResult> { new DefaultWebTestResult(string.Empty, string.Empty, DateTime.Now, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty) };
			
			//get the environment
            Item ei = TestStar.Core.Utility.Constants.MasterDB.GetItem(EnvironmentID);
			if (ei == null) {
                errorList[0].Message = TextProviderPaths.Errors.TestRunner.NullEnv(tProvider);
				return errorList;
			}
            IEnvironmentProvider eProvider = (IEnvironmentProvider)new SCEnvironmentProvider(tProvider);
            ITestEnvironment te = eProvider.GetTestEnvironment(ei.ID.ToString(), ei.DisplayName, ei.GetSafeFieldValue("DomainPrefix"), ei.GetSafeFieldValue("IPAddress"));

			//get the site
            Item si = TestStar.Core.Utility.Constants.MasterDB.GetItem(SiteID);
			if (si == null) {
                errorList[0].Message = TextProviderPaths.Errors.TestRunner.NullSite(tProvider);
				return errorList;
			}
            SCSiteProvider sProvider = new SCSiteProvider(eProvider, tProvider);
            ITestSite ts = sProvider.GetTestSite(si);

			//get the test fixture
            TestFixture tf = TestUtility.GetTestSuite(AssemblyName).GetFixtures().Where(a => a.ClassName.Equals(ClassName)).FirstOrDefault();
            if (tf == null) {
                errorList[0].Message = TextProviderPaths.Errors.TestRunner.NullTest(tProvider);
				return errorList;
			}

            manager.RunTest(tf, te, ts);

            List<DefaultWebTestResult> rl = manager.ResultList;
            foreach (DefaultWebTestResult d in rl)
                d.ID = SitecoreUtility.CreateResultEntry(d);
            
            return manager.ResultList;
        }

		[WebMethod]
		///@TestCalls is a list of strings in the format AssemblyName::ClassName
		public List<DefaultWebTestResult> RunWebTests(string EnvironmentID, string SiteID, List<string> TestCalls) {
			List<DefaultWebTestResult> resultSet = new List<DefaultWebTestResult>();
			foreach (string s in TestCalls) {
				string[] arr = s.Split(new string[] { "::" }, StringSplitOptions.RemoveEmptyEntries);
				if (arr.Length < 2)
					continue;
				resultSet.AddRange(RunWebTest(EnvironmentID, SiteID, arr[0], arr[1]));
			}
			return resultSet;
		}

		[WebMethod]
		///@TestCalls is a list of strings in the format AssemblyName::Category
		public GenScriptResult CreateUnitTestScript(string ScriptName, List<string> TestCalls) {
            
            SCTextEntryProvider tProvider = new SCTextEntryProvider();
            
			if (string.IsNullOrEmpty(ScriptName))
                return new GenScriptResult(false, TextProviderPaths.Errors.ScriptGen.ScriptGenNameNull(tProvider));
		
			if (!TestCalls.Any())
                return new GenScriptResult(false, TextProviderPaths.Errors.ScriptGen.ScriptGenNoCalls(tProvider));

			Dictionary<string, List<string>> testSet = new Dictionary<string, List<string>>();
			//define exe, assembly, categories and name(blank)
			foreach (string s in TestCalls) {
				string[] arr = s.Split(new string[] { "::" }, StringSplitOptions.RemoveEmptyEntries);
				if (arr.Length < 2)
					continue;
				if (!testSet.ContainsKey(arr[0]))
					testSet[arr[0]] = new List<string>();
				testSet[arr[0]].Add(arr[1]);
			}

			StringBuilder sb = new StringBuilder();
			sb.Append("#UNIT TESTING#").AppendLine().AppendLine();
			sb.Append("$testTable = @{}").AppendLine().AppendLine();
			
			int i = 1;
			foreach (KeyValuePair<string, List<string>> kvp in testSet) {
				string entryName = string.Format("$testCats{0}", i);
				sb.AppendFormat("{0} = New-Object System.Collections.ArrayList", entryName).AppendLine();
				foreach (string s in kvp.Value) {
					sb.AppendFormat("$length = {0}.Add(\"{1}\");", entryName, s).AppendLine();
				}
				sb.AppendLine().AppendFormat("$testTable.Add(\"{0}\", {1})", kvp.Key, entryName).AppendLine().AppendLine();
				i++;
			}

			sb.AppendFormat("$URI = '{0}'", HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)).AppendLine().AppendLine();

			sb.Append("$URI += '/sitecore%20modules/Web/teststar/service/testservice.asmx'").AppendLine();
			sb.Append("$proxy = New-WebServiceProxy -Uri $URI -Namespace System -Class string").AppendLine();
			sb.Append("$errList = New-Object System.Collections.ArrayList").AppendLine().AppendLine();

			sb.Append("foreach ($t in $testTable.GetEnumerator()) {").AppendLine();
			sb.Append("  foreach ($c in $t.Value) {").AppendLine();
			sb.Append("    [json]$response = $proxy.RunUnitTests($t.Key, $c)").AppendLine();
			sb.Append("    $errs = $response | where {$_.Failed -eq $True}").AppendLine();
			sb.Append("    foreach($e in $errs){").AppendLine();
			sb.Append("      $a = $errList.Add($e.Message)").AppendLine();
			sb.Append("    }").AppendLine();
			sb.Append("  }").AppendLine();
			sb.Append("}").AppendLine().AppendLine();

			sb.Append("if($errList.Count -gt 0){").AppendLine();
			sb.Append("  $out = 'There was ' + $errList.Count + ' error(s)'").AppendLine();
			sb.Append("  Write-Output $out").AppendLine();
			sb.Append("  exit(1)").AppendLine();
			sb.Append("} else {").AppendLine();
			sb.Append("  exit(0)").AppendLine();
			sb.Append("}");

			//write file
			string filePath = string.Format(@"{0}sitecore modules\web\teststar\scripts\{1}.ps1", Cons.ApplicationRoot, ScriptName);
			using (StreamWriter newData = new StreamWriter(filePath, false)) {
				newData.WriteLine(sb.ToString());
			}

            return new GenScriptResult(true, string.Format(TextProviderPaths.Messages.ScriptGen.ScriptGenSuccess(tProvider), filePath));
		}

		[WebMethod]
		public GenScriptResult CreateWebTestScript(string ScriptName, List<string> TestCalls, List<string> EnvironmentIDs, List<string> SiteIDs) {
            
            SCTextEntryProvider tProvider = new SCTextEntryProvider();
            
            if (string.IsNullOrEmpty(ScriptName))
                return new GenScriptResult(false, TextProviderPaths.Errors.ScriptGen.ScriptGenNameNull(tProvider));

			if (!TestCalls.Any())
                return new GenScriptResult(false, TextProviderPaths.Errors.ScriptGen.ScriptGenNoCalls(tProvider));

			StringBuilder sb = new StringBuilder();
			sb.Append("#WEB TESTING#").AppendLine().AppendLine();

			sb.Append("$tests = New-Object System.Collections.ArrayList").AppendLine();
			foreach (string t in TestCalls) {
				sb.AppendFormat("$length = $tests.Add(\"{0}\");", t).AppendLine();
			}

			sb.AppendLine().Append("$envs =  New-Object System.Collections.ArrayList").AppendLine();
			foreach (string e in EnvironmentIDs) {
				sb.AppendFormat("$length = $envs.Add(\"{0}\");", e).AppendLine();
			}

			sb.AppendLine().Append("$sites = New-Object System.Collections.ArrayList").AppendLine();
			foreach (string s in SiteIDs) {
				sb.AppendFormat("$length = $sites.Add(\"{0}\");", s).AppendLine();
			}

			sb.AppendLine().AppendFormat("$URI = '{0}'", HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)).AppendLine().AppendLine();
			
			sb.Append("$URI += '/sitecore%20modules/Web/teststar/service/testservice.asmx'").AppendLine();
			sb.Append("$proxy = New-WebServiceProxy -Uri $URI -Namespace System -Class string").AppendLine();
			sb.Append("$errList = New-Object System.Collections.ArrayList").AppendLine().AppendLine();

			sb.Append("foreach ($e in $envs) {").AppendLine();
			sb.Append("  foreach ($s in $sites) {").AppendLine();
			sb.Append("    [json]$response = $proxy.RunWebTests($e, $s, $tests)").AppendLine();
			sb.Append("    $errs = $response | where {$_.Failed -eq $True}").AppendLine();
			sb.Append("    foreach($e in $errs){").AppendLine();
			sb.Append("      $a = $errList.Add($e.Message)").AppendLine();
			sb.Append("    }").AppendLine();
			sb.Append("  }").AppendLine();
			sb.Append("}").AppendLine().AppendLine();

			sb.Append("if($errList.Count -gt 0){").AppendLine();
			sb.Append("  $out = 'There was ' + $errList.Count + ' error(s)'").AppendLine();
			sb.Append("  Write-Output $out").AppendLine();
			sb.Append("  exit(1)").AppendLine();
			sb.Append("} else {").AppendLine();
			sb.Append("  exit(0)").AppendLine();
			sb.Append("}");

			//write file
			string filePath = string.Format(@"{0}sitecore modules\web\TestStar\scripts\{1}.ps1", Cons.ApplicationRoot, ScriptName);
			using (StreamWriter newData = new StreamWriter(filePath, false)) {
				newData.WriteLine(sb.ToString());
			}
            return new GenScriptResult(true, string.Format(TextProviderPaths.Messages.ScriptGen.ScriptGenSuccess(tProvider), filePath));
		}
	}
}
