using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Utility;
using Cons = Sitecore.TestStar.Core.Utility.Constants;
using Sitecore.TestStar.Core.Providers.Interfaces;
using Sitecore.TestStar.Core.Entities.Interfaces;
using Sitecore.Configuration;

namespace Sitecore.TestStar.Core.Providers {
	public class SCSystemProvider : ISystemProvider {

		public IEnumerable<ITestSystem> GetSystems() {
			Item folder = Cons.MasterDB.GetItem(Settings.GetSetting("TestStar.SiteFolder"));
			if (folder == null)
				throw new NullReferenceException(SCTextEntryProvider.Exceptions.Providers.SiteFoldNull);

            if (!folder.HasChildren)
                return Enumerable.Empty<ITestSystem>();

			IEnumerable<ITestSystem> systems = from Item i in folder.GetChildren()
                                               where i.TemplateID.ToString().Equals(Settings.GetSetting("TestStar.SystemTemplate"))
                                               select GetTestSystem(i.ID.ToString(), i.DisplayName);
			return systems;
		}

        public ITestSystem GetTestSystem(string id, string name) {
            return new DefaultTestSystem(id, name);
        }
	}
}
