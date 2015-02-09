using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.Core.Entities.Interfaces {
    public interface ITestResultList {

        string ID { get; set; }
        string Title { get; set; }
        DateTime Date { get; set; }
        IEnumerable<ITestResult> ResultEntries { get; set; }
    }
}
