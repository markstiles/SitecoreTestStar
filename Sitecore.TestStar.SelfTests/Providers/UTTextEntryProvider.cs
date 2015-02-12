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
using Sitecore.TestStar.Core.Providers.Interfaces;
using Sitecore.Configuration;

namespace Sitecore.TestStar.Core.Providers {
	public class UTTextEntryProvider : ITextEntryProvider {
		
        public string GetTextByKey(string TextKey) {
			return TextKey;
		}
    }
}
