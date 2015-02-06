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
using Sitecore.TestStar.Core.Providers.Interfaces;

namespace Sitecore.TestStar.Core.Providers {
	public class SCEnvironmentProvider : IEnvironmentProvider {

		public IEnumerable<TestEnvironment> GetEnvironments() {
			Item folder = Cons.MasterDB.GetItem(Cons.EnvironmentFolder);
			if(folder == null)
				throw new NullReferenceException(SCTextEntryProvider.Exceptions.Providers.EnvFoldNull);

            if (!folder.HasChildren)
                return Enumerable.Empty<TestEnvironment>();

			IEnumerable<TestEnvironment> environments = from Item i in folder.GetChildren()
														select FillTestEnvironment(i);
			return environments;
		}

        public TestEnvironment FillTestEnvironment(Item i) {
            return new TestEnvironment(i.ID.ToString(), i.DisplayName, i.GetSafeFieldValue("DomainPrefix"), i.GetSafeFieldValue("IPAddress"));
        }
	}
}
