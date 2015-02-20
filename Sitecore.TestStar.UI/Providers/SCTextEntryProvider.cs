using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data;
using Sitecore.Data.Items;
using Cons = Sitecore.TestStar.Core.Utility.Constants;
using Sitecore.TestStar.Core.Utility;
using Sitecore.TestStar.Core.Extensions;
using Sitecore.TestStar.Core.Providers;
using Sitecore.Configuration;

namespace Sitecore.TestStar.UI.Providers {
	public class SCTextEntryProvider : ITextEntryProvider {
		
        public string GetTextByKey(string TextKey) {

            Item folder = Cons.MasterDB.GetItem(Settings.GetSetting("TestStar.TextDictionary"));
			if (folder == null)
				throw new NullReferenceException();

            Item i = Cons.MasterDB.GetItem(string.Format("{0}{1}", folder.Paths.Path, TextKey));
            return (i != null) ? i.GetSafeFieldValue("Value") : string.Empty;
		}
    }
}
