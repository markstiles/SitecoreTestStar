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
using Sitecore.TestStar.Core.Entities.Interfaces;
using Sitecore.Configuration;

namespace Sitecore.TestStar.Core.Providers {
	public class SCEnvironmentProvider : IEnvironmentProvider {
        
        private ITextEntryProvider TextProvider;

        public SCEnvironmentProvider(ITextEntryProvider t) {
            if (t == null)
                throw new NullReferenceException();
            TextProvider = t;
        }

        public IEnumerable<ITestEnvironment> GetEnvironments() {
            Item folder = Cons.MasterDB.GetItem(Settings.GetSetting("TestStar.EnvironmentFolder"));
			if(folder == null)
				throw new NullReferenceException(TextProviderPaths.Exceptions.Providers.EnvFoldNull(TextProvider));

            if (!folder.HasChildren)
                return Enumerable.Empty<ITestEnvironment>();

			IEnumerable<ITestEnvironment> environments = from Item i in folder.GetChildren()
                                                         select GetTestEnvironment(i.ID.ToString(), i.DisplayName, i.GetSafeFieldValue("DomainPrefix"), i.GetSafeFieldValue("IPAddress"));
			return environments;
		}

        public ITestEnvironment GetTestEnvironment(string id, string name, string domainPrefix, string ipAddress) {
            return (ITestEnvironment)new DefaultTestEnvironment(id, name, domainPrefix, ipAddress);
        }
	}
}
