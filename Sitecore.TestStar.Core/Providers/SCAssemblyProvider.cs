using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.TestStar.Core.Utility;
using Cons = Sitecore.TestStar.Core.Utility.Constants;
using Sitecore.TestStar.Core.Extensions;
using Sitecore.TestStar.Core.Providers.Interfaces;

namespace Sitecore.TestStar.Core.Providers {
	public class SCAssemblyProvider : IAssemblyProvider {

        public IEnumerable<string> GetUnitTestAssemblies() {
			Item folder = Cons.MasterDB.GetItem(Cons.UnitAssemblies);
			if (folder == null)
				throw new NullReferenceException(SCTextEntryProvider.Exceptions.Providers.UnitFoldNull);

            if (!folder.HasChildren)
                return Enumerable.Empty<string>();

			IEnumerable<string> assemblies = from Item i in folder.GetChildren()
                                             select i.GetSafeFieldValue("AssemblyName");
			return assemblies.Where(a => !string.IsNullOrEmpty(a) && File.Exists(string.Format(@"{0}\{1}.dll", Cons.ExecutionRoot, a)));
		}

        public IEnumerable<string> GetWebTestAssemblies() {
			Item folder = Cons.MasterDB.GetItem(Cons.WebAssemblies);
			if (folder == null)
				throw new NullReferenceException(SCTextEntryProvider.Exceptions.Providers.WebFoldNull);

            if (!folder.HasChildren)
                return Enumerable.Empty<string>();

            IEnumerable<string> assemblies = from Item i in folder.GetChildren()
                                             select i.GetSafeFieldValue("AssemblyName");
			return assemblies.Where(a => !string.IsNullOrEmpty(a) && File.Exists(string.Format(@"{0}\{1}.dll", Cons.ExecutionRoot, a)));
		}
	}
}
