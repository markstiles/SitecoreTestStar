using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.Core.Providers {
    public interface ITextEntryProvider {

        string GetTextByKey(string TextKey);
    }
}
