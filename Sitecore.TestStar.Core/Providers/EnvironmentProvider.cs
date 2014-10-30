using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Utility;

namespace Sitecore.TestStar.Core.Providers {
	public class EnvironmentProvider {

		public static IEnumerable<TestEnvironment> GetEnvironments() {
			IEnumerable<TestEnvironment> environments = JsonSerializer.GetObject<List<TestEnvironment>>(filePath);
			if (environments == null)
				throw new NullReferenceException(
					string.Format("Sitecore.TestStar.Core.EnvironmentProvider.GetEnvironments: Check that the file path [{0}] exists and that it's not malformed json.",
					filePath
					)
				);
			return environments;
		}
	}
}
