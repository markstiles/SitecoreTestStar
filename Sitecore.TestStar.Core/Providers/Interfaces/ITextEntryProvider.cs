using Sitecore.Data;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.Core.Providers.Interfaces {
    public interface ITextEntryProvider {

        string GetTextByKey(string TextKey);
		
        string GetTextByKey(string TextKey, Database db);

        string FillTextEntry(Item i);
    }
}
