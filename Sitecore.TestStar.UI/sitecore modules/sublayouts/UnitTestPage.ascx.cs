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

namespace Sitecore.TestStar.Core.UI.sublayouts {
	public partial class UnitTestPage : UserControl {

		#region Events

		protected void Page_Load(object sender, EventArgs e) {
			
			// Initialise NUnit
			CoreExtensions.Host.InitializeService();

            SCTextEntryProvider tProvider = new SCTextEntryProvider();
            IAssemblyProvider aProvider = (IAssemblyProvider)new SCAssemblyProvider(tProvider);

			if (!IsPostBack) {
                rptSuites.DataSource = TestUtility.GetUnitTestSuites(aProvider);
				rptSuites.DataBind();
			}
		}

        protected void rptSuites_ItemDataBound(object sender, RepeaterItemEventArgs e) {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;

            KeyValuePair<string, TestSuite> profile = (KeyValuePair<string, TestSuite>)e.Item.DataItem;

			Repeater rptCategories = (Repeater)e.Item.FindControl("rptCategories");
			rptCategories.DataSource = from string t in profile.Value.GetAllCategories().OrderBy(a => a) select new ListItem(t, profile.Key);
			rptCategories.DataBind();
        }

		protected string CondenseCatName(string className) {
			return className.Replace(" ", string.Empty);
		}
		
		#endregion Events
	}
}
