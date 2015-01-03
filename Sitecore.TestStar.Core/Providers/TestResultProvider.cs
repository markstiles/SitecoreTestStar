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
		
		public static IEnumerable<TestResultList> GetResults() {
			Item folder = Cons.MasterDB.GetItem(Cons.ResultsFolder);
			if (folder == null)
				throw new NullReferenceException(Cons.Exceptions.ResultFoldNull);

			if (!folder.HasChildren)
				return Enumerable.Empty<TestResultList>();

			IEnumerable<TestResultList> results = from Item i in folder.Axes.GetDescendants()
										  where i.TemplateID.ToString().Equals(Cons.ResultsListTemplate)
										  select Factory.GetTestResult(i);

			return results.OrderByDescending(a => a.Date);
		}
	}
}
