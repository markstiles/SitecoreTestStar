using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.TestStar.Core.Entities;
using Sitecore.TestStar.Core.Utility;

namespace Sitecore.TestStar.Core.Providers {
	public class SystemProvider {

		public static IEnumerable<TestSystem> GetSystems() {
			IEnumerable<TestSystem> systems = JsonSerializer.GetObject<List<TestSystem>>(filePath);
			if (systems == null)
				throw new NullReferenceException("Sitecore.TestStar.Core.SystemProvider.GetSystems: Check the file path specified exists and that it's not malformed json.");
			return systems;
		}
	}
}
