using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Extensions;
using Sitecore.TestStar.Core.Utility;
using Cons = Sitecore.TestStar.Core.Utility.Constants;

namespace Sitecore.TestStar.Core.Providers {
	public class EnvironmentProvider {

		public static IEnumerable<TestEnvironment> GetEnvironments() {
			Item folder = Cons.MasterDB.GetItem(Cons.EnvironmentFolder);
			if(folder == null)
				throw new NullReferenceException(Cons.Exceptions.EnvFoldNull);
			IEnumerable<TestEnvironment> environments = from Item i in folder.GetChildren()
														select Factory.GetTestEnvironment(i);
			return environments;
		}
	}
}
