using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Utility;
using Sitecore.TestStar.Core.Providers;
using Sitecore.TestStar.Core.Entities.Interfaces;
using Sitecore.Configuration;
using Sitecore.TestStar.UI.Utility;

namespace Sitecore.TestStar.UI.Providers {
	public class SCSystemProvider : ISystemProvider {

        private ISiteProvider SiteProvider;
        private ITextEntryProvider TextProvider;

        public SCSystemProvider(ISiteProvider s, ITextEntryProvider t) {
            if (s == null)
                throw new NullReferenceException();
            SiteProvider = s;
            if (t == null)
                throw new NullReferenceException();
            TextProvider = t;
        }

		public IEnumerable<ITestSystem> GetSystems() {
			Item folder = SitecoreUtility.MasterDB.GetItem(Settings.GetSetting("TestStar.SiteFolder"));
			if (folder == null)
				throw new NullReferenceException(TextProviderPaths.Exceptions.Providers.SiteFoldNull(TextProvider));

            if (!folder.HasChildren)
                return Enumerable.Empty<ITestSystem>();

			IEnumerable<ITestSystem> systems = from Item i in folder.GetChildren()
                                               where i.TemplateID.ToString().Equals(Settings.GetSetting("TestStar.SystemTemplate"))
                                               select GetTestSystem(i.ID.ToString(), i.DisplayName);
			return systems;
		}

        public ITestSystem GetTestSystem(string id, string name) {
            return new DefaultTestSystem(id, name, SiteProvider);
        }
	}
}
