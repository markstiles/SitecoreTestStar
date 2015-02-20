using Sitecore.TestStar.Core.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.TestStar.Core.Utility {
    public class TextProviderPaths {
        
        public static class Exceptions {
            public static class Providers {
                public static string EnvFoldNull(ITextEntryProvider t) {
                    return t.GetTextByKey("/Exceptions/Providers/EnvFoldNull");
                }
                public static string ResultFoldNull(ITextEntryProvider t) {
                    return t.GetTextByKey("/Exceptions/Providers/ResultFoldNull");
                }
                public static string SiteFoldNull(ITextEntryProvider t) {
                    return t.GetTextByKey("/Exceptions/Providers/SiteFoldNull");
                }
                public static string TextDicNull(ITextEntryProvider t) {
                    return t.GetTextByKey("/Exceptions/Providers/TextDicNull");
                }
                public static string UnitFoldNull(ITextEntryProvider t) {
                    return t.GetTextByKey("/Exceptions/Providers/UnitFoldNull");
                }
                public static string WebFoldNull(ITextEntryProvider t) {
                    return t.GetTextByKey("/Exceptions/Providers/WebFoldNull");
                }
            }

            public static class Util {
                public static string NullJSON(ITextEntryProvider t) {
                    return t.GetTextByKey("/Exceptions/Util/NullJSON");
                }
            }

            public static class Managers {
                public static string IUnitTestHandlerNull(ITextEntryProvider t) {
                    return t.GetTextByKey("/Exceptions/Managers/IUnitTestHandlerNull");
                }
                public static string IWebTestHandlerNull(ITextEntryProvider t) {
                    return t.GetTextByKey("/Exceptions/Managers/IWebTestHandlerNull");
                }
                public static string TestFixtureNull(ITextEntryProvider t) {
                    return t.GetTextByKey("/Exceptions/Managers/TestFixtureNull");
                }
                public static string TestMethodNull(ITextEntryProvider t) {
                    return t.GetTextByKey("/Exceptions/Managers/TestMethodNull");
                }
            }
        }

        public static class Errors {
            public static class TestRunner {
                public static string NoEnvs(ITextEntryProvider t) {
                    return t.GetTextByKey("/Errors/TestRunner/NoEnvs");
                }
                public static string NoSites(ITextEntryProvider t) {
                    return t.GetTextByKey("/Errors/TestRunner/NoSites");
                }
                public static string NoTests(ITextEntryProvider t) {
                    return t.GetTextByKey("/Errors/TestRunner/NoTests");
                }
                public static string NullEnv(ITextEntryProvider t) {
                    return t.GetTextByKey("/Errors/TestRunner/NullEnv");
                }
                public static string NullSite(ITextEntryProvider t) {
                    return t.GetTextByKey("/Errors/TestRunner/NullSite");
                }
                public static string NullTest(ITextEntryProvider t) {
                    return t.GetTextByKey("/Errors/TestRunner/NullTest");
                }
            }

            public static class ScriptGen {
                public static string NoScriptName(ITextEntryProvider t) {
                    return t.GetTextByKey("/Errors/ScriptGen/NoScriptName");
                }
                public static string ScriptGenNameNull(ITextEntryProvider t) {
                    return t.GetTextByKey("/Errors/ScriptGen/WebFoldNull");
                }
                public static string ScriptGenNoCalls(ITextEntryProvider t) {
                    return t.GetTextByKey("/Errors/ScriptGen/WebFoldNull");
                }
            }

            public static class Webtests {
                public static string Actual(ITextEntryProvider t) {
                    return t.GetTextByKey("/Errors/Webtests/Actual");
                }
                public static string Expected(ITextEntryProvider t) {
                    return t.GetTextByKey("/Errors/Webtests/Expected");
                }
                public static string NotRedirect(ITextEntryProvider t) {
                    return t.GetTextByKey("/Errors/Webtests/NotRedirect");
                }
                public static string SitemapEmpty(ITextEntryProvider t) {
                    return t.GetTextByKey("/Errors/Webtests/SitemapEmpty");
                }
                public static string SitemapLinkNotFound(ITextEntryProvider t) {
                    return t.GetTextByKey("/Errors/Webtests/SitemapLinkNotFound");
                }
                public static string SitemapNoLinks(ITextEntryProvider t) {
                    return t.GetTextByKey("/Errors/Webtests/SitemapNoLinks");
                }
                public static string SitemapNotFound(ITextEntryProvider t) {
                    return t.GetTextByKey("/Errors/Webtests/SitemapNotFound");
                }
                public static string Was(ITextEntryProvider t) {
                    return t.GetTextByKey("/Errors/Webtests/Was");
                }
            }
        }
        
        public static class Messages {
            public static class ScriptGen {
                public static string ScriptGenSuccess(ITextEntryProvider t) {
                    return t.GetTextByKey("/Messages/ScriptGen/ScriptGenSuccess");
                }
            }
        }
        
        public static class Page {
            public static string Environments(ITextEntryProvider t) {
                return t.GetTextByKey("/Page/Environments");
            }
            public static string GenerateScript(ITextEntryProvider t) {
                return t.GetTextByKey("/Page/GenerateScript");
            }
            public static string Results(ITextEntryProvider t) {
                return t.GetTextByKey("/Page/Results");
            }
            public static string Run(ITextEntryProvider t) {
                return t.GetTextByKey("/Page/Run");
            }
            public static string ScriptName(ITextEntryProvider t) {
                return t.GetTextByKey("/Page/ScriptName");
            }
            public static string Sites(ITextEntryProvider t) {
                return t.GetTextByKey("/Page/Sites");
            }
            public static string Systems(ITextEntryProvider t) {
                return t.GetTextByKey("/Page/Systems");
            }
            public static string TestSettings(ITextEntryProvider t) {
                return t.GetTextByKey("/Page/TestSettings");
            }
            public static string TestSelect(ITextEntryProvider t) {
                return t.GetTextByKey("/Page/TestSelect");
            }
            public static string TestDeselect(ITextEntryProvider t) {
                return t.GetTextByKey("/Page/TestDeselect");
            }
            public static string TestSuites(ITextEntryProvider t) {
                return t.GetTextByKey("/Page/TestSuites");
            }
            public static string UnitTestSuites(ITextEntryProvider t) {
                return t.GetTextByKey("/Page/UnitTestSuites");
            }
        }

        public static class ResultList {
            public static string Error(ITextEntryProvider t) {
                return t.GetTextByKey("/ResultList/Error");
            }
            public static string Failure(ITextEntryProvider t) {
                return t.GetTextByKey("/ResultList/Failure");
            }
            public static string NextBtn(ITextEntryProvider t) {
                return t.GetTextByKey("/ResultList/NextBtn");
            }
            public static string NoResults(ITextEntryProvider t) {
                return t.GetTextByKey("/ResultList/NoResults");
            }
            public static string PrevBtn(ITextEntryProvider t) {
                return t.GetTextByKey("/ResultList/PrevBtn");
            }
            public static string Skipped(ITextEntryProvider t) {
                return t.GetTextByKey("/ResultList/Skipped");
            }
            public static string Success(ITextEntryProvider t) {
                return t.GetTextByKey("/ResultList/Success");
            }
        }
    }
}
