using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Utility;
using Cons = Sitecore.TestStar.Core.Utility.Constants;

namespace Sitecore.TestStar.Core.Providers {
	public class ResultProvider {
		
		public static IEnumerable<TestResultEntry> GetResults() {
			Item folder = Cons.MasterDB.GetItem(Cons.ResultsFolder);
			if (folder == null)
				throw new NullReferenceException(Cons.Exceptions.ResultFoldNull);

			if (!folder.HasChildren)
				return Enumerable.Empty<TestResultEntry>();

			IEnumerable<TestResultEntry> results = from Item i in folder.Axes.GetDescendants()
										  where i.TemplateID.ToString().Equals(Cons.ResultsTemplate)
										  select Factory.GetTestResult(i);

			return results.OrderByDescending(a => a.Date);
		}

		public static IEnumerable<TestResultEntry> GetUnitTestResults() {
			return GetResults().Where(a => a.IsUnitTest);
		}

		public static IEnumerable<TestResultEntry> GetWebTestResults() {
			return GetResults().Where(a => !a.IsUnitTest);
		}
	}
}
