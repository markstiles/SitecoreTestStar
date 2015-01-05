using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data;
using Sitecore.Data.Items;
using Cons = Sitecore.TestStar.Core.Utility.Constants;

namespace Sitecore.TestStar.Core.Providers {
	public class TextEntryProvider {
		
		public static string GetTextByKey(string TextKey) {
			return GetTextByKey(TextKey, Sitecore.Context.Database);
		}

		public static string GetTextByKey(string TextKey, Database db) {

			Item folder = Cons.MasterDB.GetItem(Cons.TextDictionary);
			if (folder == null)
				throw new NullReferenceException(Exceptions.Providers.TextDicNull);

			Item i = db.GetItem(string.Format("{0}{1}", folder.Paths.Path, TextKey));
			return (i != null) ? i["Value"] : string.Empty;
		}

		#region Exception Messages

		public static class Exceptions {
			public static class Providers {
				public static string EnvFoldNull {
					get {
						return GetTextByKey("/Exceptions/Providers/EnvFoldNull");
					}
				}
				public static string ResultFoldNull {
					get {
						return GetTextByKey("/Exceptions/Providers/ResultFoldNull");
					}
				}
				public static string SiteFoldNull {
					get {
						return GetTextByKey("/Exceptions/Providers/SiteFoldNull");
					}
				}
				public static string TextDicNull {
					get {
						return GetTextByKey("/Exceptions/Providers/TextDicNull");
					}
				}
				public static string UnitFoldNull {
					get {
						return GetTextByKey("/Exceptions/Providers/UnitFoldNull");
					}
				}
				public static string WebFoldNull {
					get {
						return GetTextByKey("/Exceptions/Providers/WebFoldNull");
					}
				}
			}

			public static class Util {
				public static string NullJSON {
					get {
						return GetTextByKey("/Exceptions/Util/NullJSON");
					}
				}
			}

			public static class Managers {
				public static string IUnitTestHandlerNull {
					get {
						return GetTextByKey("/Exceptions/Managers/IUnitTestHandlerNull");
					}
				}
				public static string IWebTestHandlerNull {
					get {
						return GetTextByKey("/Exceptions/Managers/IWebTestHandlerNull");
					}
				}
				public static string TestFixtureNull {
					get {
						return GetTextByKey("/Exceptions/Managers/TestFixtureNull");
					}
				}
				public static string TestMethodNull {
					get {
						return GetTextByKey("/Exceptions/Managers/TestMethodNull");
					}
				}
			}
		}

		public static class Errors {
			public static class TestRunner {
				public static string NullEnv {
					get {
						return GetTextByKey("/Errors/TestRunner/WebFoldNull");
					}
				}
				public static string NullSite {
					get {
						return GetTextByKey("/Errors/TestRunner/WebFoldNull");
					}
				}
				public static string NullTest {
					get {
						return GetTextByKey("/Errors/TestRunner/WebFoldNull");
					}
				}
			}
			
			public static class ScriptGen {
				public static string ScriptGenNameNull {
				get {
					return GetTextByKey("/Errors/ScriptGen/WebFoldNull");
				}
			}
				public static string ScriptGenNoCalls {
					get {
						return GetTextByKey("/Errors/ScriptGen/WebFoldNull");
					}
				}
			}
		}

		public static class Messages {
			public static class ScriptGen {
				public static string ScriptGenSuccess {
					get {
						return GetTextByKey("/Messages/ScriptGen/ScriptGenSuccess");
					}
				}
			}
		}

		public static class Page {
			public static string Environments {
				get {
					return GetTextByKey("/Page/Environments");
				}
			}
			public static string GenerateScript {
				get {
					return GetTextByKey("/Page/GenerateScript");
				}
			}
			public static string Results {
				get {
					return GetTextByKey("/Page/Results");
				}
			}
			public static string Run {
				get {
					return GetTextByKey("/Page/Run");
				}
			}
			public static string ScriptName {
				get {
					return GetTextByKey("/Page/ScriptName");
				}
			}
			public static string Sites {
				get {
					return GetTextByKey("/Page/Sites");
				}
			}
			public static string Systems {
				get {
					return GetTextByKey("/Page/Systems");
				}
			}
			public static string TestSettings {
				get {
					return GetTextByKey("/Page/TestSettings");
				}
			}
			public static string TestSuites {
				get {
					return GetTextByKey("/Page/TestSuites");
				}
			}
			public static string UnitTestSuites {
				get {
					return GetTextByKey("/Page/UnitTestSuites");
				}
			}
		}

		public static class ResultList {
			public static string NextBtn {
				get {
					return GetTextByKey("/ResultList/NextBtn");
				}
			}
			public static string PrevBtn {
				get {
					return GetTextByKey("/ResultList/PrevBtn");
				}
			}
			public static string NoResults {
				get {
					return GetTextByKey("/ResultList/NoResults");
				}
			}
		}

		#endregion Exception Messages
	}
}
