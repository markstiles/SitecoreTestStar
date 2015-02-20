using Sitecore.Data.Items;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.Core.Providers {
    public interface ITestResultProvider {

        IEnumerable<ITestResultList> GetTestResultLists();

        ITestResultList GetTestResultList(string id, string name, DateTime date, List<ITestResult> entries);
    }
}
