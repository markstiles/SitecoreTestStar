using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.TestStar.Core.Utility;
using Cons = Sitecore.TestStar.Core.Utility.Constants;

namespace Sitecore.TestStar.Core.Providers {
	public static class AssemblyProvider {

		public static IEnumerable<string> GetUnitTestAssemblies() {
			Item folder = Cons.MasterDB.GetItem(Cons.UnitAssemblies);
			if (folder == null)
				throw new NullReferenceException(TextEntryProvider.Exceptions.Providers.UnitFoldNull);

            if (!folder.HasChildren)
                return Enumerable.Empty<string>();

			IEnumerable<string> assemblies = from Item i in folder.GetChildren()
											 select Factory.GetTestAssembly(i);
			return assemblies.Where(a => !string.IsNullOrEmpty(a) && File.Exists(string.Format(@"{0}\{1}.dll", Cons.ExecutionRoot, a)));
		}

		public static IEnumerable<string> GetWebTestAssemblies() {
			Item folder = Cons.MasterDB.GetItem(Cons.WebAssemblies);
			if (folder == null)
				throw new NullReferenceException(TextEntryProvider.Exceptions.Providers.WebFoldNull);

            if (!folder.HasChildren)
                return Enumerable.Empty<string>();

            IEnumerable<string> assemblies = from Item i in folder.GetChildren()
											 select Factory.GetTestAssembly(i);
			return assemblies.Where(a => !string.IsNullOrEmpty(a) && File.Exists(string.Format(@"{0}\{1}.dll", Cons.ExecutionRoot, a)));
		}
	}
}
