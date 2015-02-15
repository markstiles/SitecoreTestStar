using System;
using System.Linq;
using System.Collections.Specialized;
using System.Data;
using System.Reflection;
using NUnit.Core;
using NUnit.Core.Filters;
using Sitecore.TestStar.Core.Utility;
using Sitecore.TestStar.Core.Extensions;
using Sitecore.TestStar.Core.Managers;
using System.Web.UI;
using Sitecore.TestStar.Core.Entities;
using System.Text;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.IO;
using Cons = Sitecore.TestStar.Core.Utility.Constants;
using Sitecore.TestStar.Core.Providers;
using Sitecore.TestStar.Core.Providers.Interfaces;

namespace Sitecore.TestStar.UI.sublayouts {
	public partial class UnitTestPage : UserControl {

		#region Events

		protected void Page_Load(object sender, EventArgs e) {
			
			// Initialise NUnit
			//CoreExtensions.Host.InitializeService();

		}

		#endregion Events
	}
}
