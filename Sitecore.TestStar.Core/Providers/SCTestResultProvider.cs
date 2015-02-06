using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Utility;
using Cons = Sitecore.TestStar.Core.Utility.Constants;
using Sitecore.TestStar.Core.Extensions;
using Sitecore.TestStar.Core.Providers.Interfaces;

namespace Sitecore.TestStar.Core.Providers {
	public class SCTestResultProvider : ITestResultProvider {
		
		public IEnumerable<TestResultList> GetResults() {
			Item folder = Cons.MasterDB.GetItem(Cons.ResultsFolder);
			if (folder == null)
				throw new NullReferenceException(SCTextEntryProvider.Exceptions.Providers.ResultFoldNull);

			if (!folder.HasChildren)
				return Enumerable.Empty<TestResultList>();

			IEnumerable<TestResultList> results = from Item i in folder.Axes.GetDescendants()
                                                  where i.TemplateID.ToString().Equals(Cons.ResultsListTemplate)
                                                  select FillTestResult(i);

			return results.OrderByDescending(a => a.Date);
		}

        public TestResultList FillTestResult(Item i) {
            return new TestResultList(i.ID.ToString(), i.DisplayName, i.GetSafeDateFieldValue("Date"));
        }
	}
}
