﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Utility;
using Cons = Sitecore.TestStar.Core.Utility.Constants;

namespace Sitecore.TestStar.Core.Providers {
	public class SystemProvider {

		public static IEnumerable<TestSystem> GetSystems() {
			Item folder = Cons.MasterDB.GetItem(Cons.EnvironmentFolder);
			if (folder == null)
				throw new NullReferenceException(Cons.Exceptions.SysFoldNull);

            if (!folder.HasChildren)
                return Enumerable.Empty<TestSystem>();

			IEnumerable<TestSystem> systems = from Item i in folder.GetChildren()
											  select Factory.GetTestSystem(i);
			return systems;
		}
	}
}
