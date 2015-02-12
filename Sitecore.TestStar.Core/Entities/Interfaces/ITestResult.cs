using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.Core.Entities.Interfaces {
    public interface ITestResult {

        string ListName { get; set; }
        string ID { get; set; }
        DateTime Date { get; set; }
        string ClassName { get; set; }
        string Method { get; set; }
        string Type { get; set; }
        string Message { get; set; }
        string AdditionalInfo { get; set; }
    }
}
