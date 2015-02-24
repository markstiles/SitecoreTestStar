using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.TestStar.Core.Utility;
using Sitecore.TestStar.Core.Extensions;
using Sitecore.TestStar.UI.Extensions;
using Cons = Sitecore.TestStar.Core.Utility.Constants;
using Sitecore.TestStar.Core.Providers;
using Sitecore.Configuration;
using Sitecore.TestStar.UI.Utility;

namespace Sitecore.TestStar.UI.Providers {
	public class SCAssemblyProvider : IAssemblyProvider {

        private ITextEntryProvider TextProvider;

        public SCAssemblyProvider(ITextEntryProvider t) {
            if (t == null)
                throw new NullReferenceException();
            TextProvider = t;
        }

        public IEnumerable<string> GetUnitTestAssemblies() {
            Item folder = SitecoreUtility.MasterDB.GetItem(Settings.GetSetting("TestStar.UnitAssemblies"));
			if (folder == null)
                throw new NullReferenceException(TextProviderPaths.Exceptions.Providers.UnitFoldNull(TextProvider));

            if (!folder.HasChildren)
                return Enumerable.Empty<string>();

			IEnumerable<string> assemblies = from Item i in folder.GetChildren()
                                             select i.GetSafeFieldValue("AssemblyName");
			return assemblies.Where(a => !string.IsNullOrEmpty(a) && File.Exists(string.Format(@"{0}\{1}.dll", Cons.ExecutionRoot, a)));
		}

        public IEnumerable<string> GetWebTestAssemblies() {
            Item folder = SitecoreUtility.MasterDB.GetItem(Settings.GetSetting("TestStar.WebAssemblies"));
			if (folder == null)
                throw new NullReferenceException(TextProviderPaths.Exceptions.Providers.WebFoldNull(TextProvider));

            if (!folder.HasChildren)
                return Enumerable.Empty<string>();

            IEnumerable<string> assemblies = from Item i in folder.GetChildren()
                                             select i.GetSafeFieldValue("AssemblyName");
			return assemblies.Where(a => !string.IsNullOrEmpty(a) && File.Exists(string.Format(@"{0}\{1}.dll", Cons.ExecutionRoot, a)));
		}
	}
}
