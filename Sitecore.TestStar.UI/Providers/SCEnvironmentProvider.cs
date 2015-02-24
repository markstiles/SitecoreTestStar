using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Extensions;
using Sitecore.TestStar.Core.Utility;
using Sitecore.TestStar.Core.Providers;
using Sitecore.TestStar.Core.Entities.Interfaces;
using Sitecore.TestStar.UI.Extensions;
using Sitecore.Configuration;
using Sitecore.TestStar.UI.Utility;

namespace Sitecore.TestStar.UI.Providers {
	public class SCEnvironmentProvider : IEnvironmentProvider {
        
        private ITextEntryProvider TextProvider;

        public SCEnvironmentProvider(ITextEntryProvider t) {
            if (t == null)
                throw new NullReferenceException();
            TextProvider = t;
        }

        public IEnumerable<ITestEnvironment> GetEnvironments() {
            Item folder = SitecoreUtility.MasterDB.GetItem(Settings.GetSetting("TestStar.EnvironmentFolder"));
			if(folder == null)
				throw new NullReferenceException(TextProviderPaths.Exceptions.Providers.EnvFoldNull(TextProvider));

            if (!folder.HasChildren)
                return Enumerable.Empty<ITestEnvironment>();

			IEnumerable<ITestEnvironment> environments = from Item i in folder.GetChildren()
                                                         where i.TemplateID.ToString().Equals(Settings.GetSetting("TestStar.EnvironmentTemplate"))
                                                         select GetTestEnvironment(i.ID.ToString(), i.DisplayName, i.GetSafeFieldValue("DomainPrefix"), i.GetSafeFieldValue("IPAddress"));
			return environments;
		}

        public ITestEnvironment GetTestEnvironment(string id, string name, string domainPrefix, string ipAddress) {
            return (ITestEnvironment)new DefaultTestEnvironment(id, name, domainPrefix, ipAddress);
        }
	}
}
