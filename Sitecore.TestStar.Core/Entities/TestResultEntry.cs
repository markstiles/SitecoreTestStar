﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.Core.Entities {
	public class TestResultEntry {

		public string ID = string.Empty;
		public string Title = string.Empty;
		public DateTime Date = DateTime.Now;
		public string Description = string.Empty;
		public string Message = string.Empty;
		public bool IsUnitTest = false;
		
		#region Constructors

		public TestResultEntry() { }

		public TestResultEntry(string id, string title, DateTime date, string description, string message, bool isUnitTest) {
			ID = id;
			Title = title;
			Date = date;
			Description = description;
			Message = message;
			IsUnitTest = isUnitTest;
		}

		#endregion Constructors

		public override bool Equals(object obj) {
			return (this.ID.Equals(((TestResultEntry)obj).ID)) ? true : false;
		}
	}
}
