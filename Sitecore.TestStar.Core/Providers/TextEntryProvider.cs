using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data;
using Sitecore.Data.Items;
using Cons = Sitecore.TestStar.Core.Utility.Constants;

namespace Sitecore.TestStar.Core.Providers {
	public class TextEntryProvider {
		
		public static string GetTextByKey(string TextKey) {
			return GetTextByKey(TextKey, Sitecore.Context.Database);
		}

		public static string GetTextByKey(string TextKey, Database db) {

			Item folder = Cons.MasterDB.GetItem(Cons.TextDictionary);
			if (folder == null)
				throw new NullReferenceException(Cons.Exceptions.TextDicNull);

			Item i = db.GetItem(string.Format("{0}{1}", folder.Paths.Path, TextKey));
			return (i != null) ? i["Value"] : string.Empty;
		}
	}
}
