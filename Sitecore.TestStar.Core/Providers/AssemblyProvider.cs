using System;
using System.Collections.Generic;
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
				throw new NullReferenceException(Cons.Exceptions.UnitFoldNull);
			IEnumerable<string> assemblies = from Item i in folder.GetChildren()
											   select Factory.GetTestAssembly(i);
			return assemblies.Where(a => !string.IsNullOrEmpty(a));
		}

		public static IEnumerable<string> GetWebTestAssemblies() {
			Item folder = Cons.MasterDB.GetItem(Cons.WebAssemblies);
			if (folder == null)
				throw new NullReferenceException(Cons.Exceptions.WebFoldNull);
			IEnumerable<string> assemblies = from Item i in folder.GetChildren()
											 select Factory.GetTestAssembly(i);
			return assemblies.Where(a => !string.IsNullOrEmpty(a));
		}
	}
}
