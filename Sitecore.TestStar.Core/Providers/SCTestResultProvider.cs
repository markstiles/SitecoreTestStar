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
using Sitecore.Data.Fields;
using Sitecore.TestStar.Core.Entities.Interfaces;
using Sitecore.Configuration;

namespace Sitecore.TestStar.Core.Providers {
	public class SCTestResultProvider : ITestResultProvider {
		
        private ITextEntryProvider TextProvider;

        public SCTestResultProvider(ITextEntryProvider t) {
            if (t == null)
                throw new NullReferenceException();
            TextProvider = t;
        }

		public IEnumerable<ITestResultList> GetTestResultLists() {
            Item folder = Cons.MasterDB.GetItem(Settings.GetSetting("TestStar.ResultsFolder"));
			if (folder == null)
				throw new NullReferenceException(TextProviderPaths.Exceptions.Providers.ResultFoldNull(TextProvider));

			if (!folder.HasChildren)
				return Enumerable.Empty<ITestResultList>();

            IEnumerable<ITestResultList> results = from Item i in folder.Axes.GetDescendants()
                                                   where i.TemplateID.ToString().Equals(Settings.GetSetting("TestStar.ResultsListTemplate"))
                                                   select GetTestResultList(i);

			return results.OrderByDescending(a => a.Date);
		}

        public ITestResultList GetTestResultList(Item i) {

            //get the entry children
            List<ITestResult> entries = new List<ITestResult>();
            List<Item> children = i.GetChildren().Reverse().ToList();
            
            foreach(Item c in children){
                ITestResult tr = null;
                if (c.TemplateID.ToString().Equals(Settings.GetSetting("TestStar.UnitTestResultTemplate"))) {
                    tr = (ITestResult)new DefaultUnitTestResult();
                } else if (c.TemplateID.ToString().Equals(Settings.GetSetting("TestStar.WebTestResultTemplate"))) {
                    DefaultWebTestResult wtr = new DefaultWebTestResult();
                    wtr.Site = c["Site"];
                    wtr.Environment = c["Environment"];
                    wtr.RequestURL = c["RequestURL"];
                    wtr.ResponseStatus = c["ResponseStatus"];
                    tr = (ITestResult)wtr;
                }

                if(tr != null) {
                    tr.ID = c.ID.ToString();
                    tr.Date = ((DateField)c.Fields["Date"]).DateTime;
                    tr.Type = c["Type"];
                    tr.Method = c["Method"];
                    tr.ClassName = c["ClassName"];
                    tr.Message = c["Message"]; 
            
                    entries.Add(tr);
                }
            }
			
            return GetTestResultList(i.ID.ToString(), i.DisplayName, i.GetSafeDateFieldValue("Date"), entries);
        }

        public ITestResultList GetTestResultList(string id, string name, DateTime date, List<ITestResult> entries) {
            return new DefaultTestResultList(id, name, date, entries);
        }
	}
}
