﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Utility;
using Cons = Sitecore.TestStar.Core.Utility.Constants;
using Sitecore.TestStar.Core.Providers.Interfaces;

namespace Sitecore.TestStar.Core.Providers {
	public class SCSystemProvider : ISystemProvider {

		public IEnumerable<TestSystem> GetSystems() {
			Item folder = Cons.MasterDB.GetItem(Cons.SiteFolder);
			if (folder == null)
				throw new NullReferenceException(SCTextEntryProvider.Exceptions.Providers.SiteFoldNull);

            if (!folder.HasChildren)
                return Enumerable.Empty<TestSystem>();

			IEnumerable<TestSystem> systems = from Item i in folder.GetChildren()
											  where i.TemplateID.ToString().Equals(Cons.SystemTemplate)
											  select FillTestSystem(i);
			return systems;
		}

        public TestSystem FillTestSystem(Item i) {
            return new TestSystem(i.ID.ToString(), i.DisplayName);
        }
	}
}