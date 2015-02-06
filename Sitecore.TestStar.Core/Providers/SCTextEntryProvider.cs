using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data;
using Sitecore.Data.Items;
using Cons = Sitecore.TestStar.Core.Utility.Constants;
using Sitecore.TestStar.Core.Utility;
using Sitecore.TestStar.Core.Extensions;
using Sitecore.TestStar.Core.Providers.Interfaces;

namespace Sitecore.TestStar.Core.Providers {
	public class SCTextEntryProvider : ITextEntryProvider {
		
		public string GetTextByKey(string TextKey) {
			return GetTextByKey(TextKey, Sitecore.Context.Database);
		}

        public string GetTextByKey(string TextKey, Database db) {

			Item folder = db.GetItem(Cons.TextDictionary);
			if (folder == null)
				throw new NullReferenceException(Exceptions.Providers.TextDicNull);

			Item i = db.GetItem(string.Format("{0}{1}", folder.Paths.Path, TextKey));
			return (i != null) ? FillTextEntry(i) : string.Empty;
		}

        public string FillTextEntry(Item i) {
            return i.GetSafeFieldValue("Value");
        }

        #region SCProvider Exception Message Shortcuts

        public static class Exceptions {
			public static class Providers {
				public static string EnvFoldNull {
					get {
                        return new SCTextEntryProvider().GetTextByKey("/Exceptions/Providers/EnvFoldNull");
					}
				}
				public static string ResultFoldNull {
					get {
                        return new SCTextEntryProvider().GetTextByKey("/Exceptions/Providers/ResultFoldNull");
					}
				}
				public static string SiteFoldNull {
					get {
                        return new SCTextEntryProvider().GetTextByKey("/Exceptions/Providers/SiteFoldNull");
					}
				}
				public static string TextDicNull {
					get {
                        return new SCTextEntryProvider().GetTextByKey("/Exceptions/Providers/TextDicNull");
					}
				}
				public static string UnitFoldNull {
					get {
                        return new SCTextEntryProvider().GetTextByKey("/Exceptions/Providers/UnitFoldNull");
					}
				}
				public static string WebFoldNull {
					get {
                        return new SCTextEntryProvider().GetTextByKey("/Exceptions/Providers/WebFoldNull");
					}
				}
			}

			public static class Util {
				public static string NullJSON {
					get {
                        return new SCTextEntryProvider().GetTextByKey("/Exceptions/Util/NullJSON");
					}
				}
			}

			public static class Managers {
				public static string IUnitTestHandlerNull {
					get {
                        return new SCTextEntryProvider().GetTextByKey("/Exceptions/Managers/IUnitTestHandlerNull");
					}
				}
				public static string IWebTestHandlerNull {
					get {
                        return new SCTextEntryProvider().GetTextByKey("/Exceptions/Managers/IWebTestHandlerNull");
					}
				}
				public static string TestFixtureNull {
					get {
                        return new SCTextEntryProvider().GetTextByKey("/Exceptions/Managers/TestFixtureNull");
					}
				}
				public static string TestMethodNull {
					get {
                        return new SCTextEntryProvider().GetTextByKey("/Exceptions/Managers/TestMethodNull");
					}
				}
			}
		}

		public static class Errors {
			public static class TestRunner {
				public static string NullEnv {
					get {
                        return new SCTextEntryProvider().GetTextByKey("/Errors/TestRunner/WebFoldNull");
					}
				}
				public static string NullSite {
					get {
                        return new SCTextEntryProvider().GetTextByKey("/Errors/TestRunner/WebFoldNull");
					}
				}
				public static string NullTest {
					get {
                        return new SCTextEntryProvider().GetTextByKey("/Errors/TestRunner/WebFoldNull");
					}
				}
			}
			
			public static class ScriptGen {
				public static string ScriptGenNameNull {
				    get {
                        return new SCTextEntryProvider().GetTextByKey("/Errors/ScriptGen/WebFoldNull");
				    }
			    }
				public static string ScriptGenNoCalls {
					get {
                        return new SCTextEntryProvider().GetTextByKey("/Errors/ScriptGen/WebFoldNull");
					}
				}
			}
		}

		public static class Messages {
			public static class ScriptGen {
				public static string ScriptGenSuccess {
					get {
                        return new SCTextEntryProvider().GetTextByKey("/Messages/ScriptGen/ScriptGenSuccess");
					}
				}
			}
		}

		public static class Page {
			public static string Environments {
				get {
                    return new SCTextEntryProvider().GetTextByKey("/Page/Environments");
				}
			}
			public static string GenerateScript {
				get {
                    return new SCTextEntryProvider().GetTextByKey("/Page/GenerateScript");
				}
			}
			public static string Results {
				get {
                    return new SCTextEntryProvider().GetTextByKey("/Page/Results");
				}
			}
			public static string Run {
				get {
                    return new SCTextEntryProvider().GetTextByKey("/Page/Run");
				}
			}
			public static string ScriptName {
				get {
					return new SCTextEntryProvider().GetTextByKey("/Page/ScriptName");
				}
			}
			public static string Sites {
				get {
                    return new SCTextEntryProvider().GetTextByKey("/Page/Sites");
				}
			}
			public static string Systems {
				get {
                    return new SCTextEntryProvider().GetTextByKey("/Page/Systems");
				}
			}
			public static string TestSettings {
				get {
                    return new SCTextEntryProvider().GetTextByKey("/Page/TestSettings");
				}
			}
			public static string TestSuites {
				get {
                    return new SCTextEntryProvider().GetTextByKey("/Page/TestSuites");
				}
			}
			public static string UnitTestSuites {
				get {
                    return new SCTextEntryProvider().GetTextByKey("/Page/UnitTestSuites");
				}
			}
		}

		public static class ResultList {
			public static string NextBtn {
				get {
                    return new SCTextEntryProvider().GetTextByKey("/ResultList/NextBtn");
				}
			}
			public static string PrevBtn {
				get {
                    return new SCTextEntryProvider().GetTextByKey("/ResultList/PrevBtn");
				}
			}
			public static string NoResults {
				get {
                    return new SCTextEntryProvider().GetTextByKey("/ResultList/NoResults");
				}
			}
        }

        #endregion SCProvider Exception Message Shortcuts
    }
}
