var animateNav;
var wTests = [];
var wPos = 0;
var uTests = [];
var uPos = 0;

function AnimateNavWidth(obj, newWidth) {
	$(obj).find("span").stop();
	$(obj).find("span").animate({ width: newWidth + "px" }, 200, function () { })
	clearInterval(animateNav);
}

$(document).ready(function () {
    
    $(".mobileNav").click(function (e) {
        if ($("body").hasClass("mNavOpen")) {
            $("body").removeClass("mNavOpen");
            window.scrollTo(0, 0);
        } else {
            $("body").addClass("mNavOpen");
        }
        $(window).scrollTop(0);
        e.preventDefault();
    });

	//set bg
	var bgNum = 4;
	var bgCur = Math.floor((Math.random() * 4) + 1);
	$("body").addClass("space"+bgCur);

	//navigation animation
	var headNavItems = $("header nav a");
	headNavItems.each(function () {
		$(this).attr("rel", $(this).find("span").width());
		$(this).find("span").width("0px");
	});
	headNavItems.hover(function () {
		var curNavItem = $(this);
		var curNavWidth = $(this).attr("rel");
		animateNav = setInterval(function () { AnimateNavWidth(curNavItem, curNavWidth) }, 150);
	}, function () {
		clearInterval(animateNav);
		$(this).find("span").animate({ width: "0px" }, 200, function () { });
	});

	//	UNIT TESTING //////////

    //unit test submit
	$(".ut #tSubmit").click(function (e) {
	    $(".resultSet").html("");
	    e.preventDefault();
	    
	    var tests = $(".testInputs input[type='checkbox']:checked");
	    var pass = UnitTestValidation(tests, "notGenerate");
	    if (!pass)
	        return;

	    uTests = [];
		uPos = 0;
		$(tests).each(function (key, value) {
			//run test through web service
	    	var data = JSON.stringify({ AssemblyName: $(value).attr("value"), Category: $(value).attr("name") });
	    	uTests.push(data);
		});
		
		UnitTestRunner();
	});

    //unit test result callback
	function RunUnitTestSuccess(data, status) {
		data.d.forEach(function (res) {
			var css = (res.Flag) ? "even" : "odd";
			var s = "<div class='result corners " + css + " " + res.Type + "'><div class='ind corners'></div>";
			if (res.ClassName != "")
				s += "<div class='rMethod'>" + res.Method + "</div>";
			var div = (res.Message.length > 0) ? ": " : "";
			s += "<div class='rResult'>" + res.ClassName + div + " " + res.Message + "</div>";
			s += "</div>";

			$(".resultSet").prepend(s);
		});

		UnitTestRunner();
	}

	//makes recursive calls while tests are available
	function UnitTestRunner() {
		if (uPos >= uTests.length) {
		    TestsCompleted();
			return;
		} else {
		    $(".resultStatus").html("");
			var per = (uPos / uTests.length) * 100;
			$(".resultCounter").text(Math.floor(per) + "%");
		}

		CallTestWS("RunUnitTests", uTests[uPos], RunUnitTestSuccess, TestError);
		uPos++;
	}

	//test generation
	$(".ut #tGenerate").click(function (e) {
		$(".resultSet").html("");
		e.preventDefault();

		var tests = $(".testInputs input[type='checkbox']:checked");
		var scriptName = $("#tScriptName").val().trim();
		var pass = UnitTestValidation(tests, scriptName);
		if (!pass)
		    return;

		var tCalls = [];
		$(tests).each(function (key, value) {
			tCalls.push($(value).attr("value") + "::" + $(value).attr("name"));
		});
        
		var data = JSON.stringify({ ScriptName: scriptName, TestCalls: tCalls });
		CallTestWS("CreateUnitTestScript", data, RunGenSuccess, TestError);
	});
		
    //form validation
	function UnitTestValidation(tests, scriptName) {
	    $(".error").hide();
	    if (tests.length == 0) {
	        $(".error").html(errNoTests).show();
	        return false;
	    } else if (scriptName == "") {
	        $(".error").html(errNoScriptName).show();
	        return false;
	    }
	    return true;
	}

	//	WEB TESTING //////////

    //if system is (un)checked then (un)check all corresponding sites
	$(".sysInputs input[type='checkbox']").click(function (e) {
	    var type = $(this).attr("name");
	    if ($(this).is(':checked'))
	        $(".wtSites input." + type).prop('checked', true);
	    else
	        $(".wtSites input." + type).prop('checked', false);
	});

    //web test submit
    $(".wt #tSubmit").click(function (e) {
        $(".resultSet").html("");
        e.preventDefault();
        
        var tests = $(".webTests input[type='checkbox']:checked");
        var envs = $(".wtEnvs input[type='checkbox']:checked");
        var sites = $(".wtSites input[type='checkbox']:checked");
        var pass = WebTestValidation(tests, envs, sites, "notGenerate")
        if (!pass)
            return;

        wTests = [];
        wPos = 0;
        for (var i = 0; i < tests.length; i++) {
            var curTest = tests[i];
            for (var j = 0; j < envs.length; j++) {
                var curEnv = envs[j];
                for (var k = 0; k < sites.length; k++) {
                    var curSite = sites[k];
                    //call web service
                    var data = JSON.stringify({ EnvironmentID: $(curEnv).attr("value"), SiteID: $(curSite).attr("value"), AssemblyName: $(curTest).attr("value"), Category: $(curTest).attr("name") });
                    wTests.push(data);
                }
            }
        }

        WebTestRunner();
	});

    //web test result callback
    function RunWebTestSuccess(data, status) {
        data.d.forEach(function (res) {
            var css = (res.Flag) ? "even" : "odd";
            var s = "<div class='result corners " + css + " " + res.Type + "'><div class='ind corners'></div>";
            s += "<div class='rSite'>" + res.Method + " - " + res.Environment + " - " + res.Site + "</div>";
            s += "<div class='clearfix'></div>";
            var div = (res.Message.length > 0) ? ": " : "";
            s += "<div class='rResult'>" + res.Type + div + " " + res.Message + "</div>";
            s += "</div>";

            $(".resultSet").prepend(s);
        });

        WebTestRunner();
    }

	//makes recursive calls while tests are available
    function WebTestRunner() {
    	if (wPos >= wTests.length) {
    	    TestsCompleted();
    		return;
    	} else {
    	    $(".resultStatus").html("");
    		var per = (wPos / wTests.length) * 100;
    		$(".resultCounter").text(Math.floor(per) + "%");
    	}

    	CallTestWS("RunWebTest", wTests[wPos], RunWebTestSuccess, TestError);
    	wPos++;
    }

	//test generation
    $(".wt #tGenerate").click(function (e) {
    	$(".resultSet").html("");
    	e.preventDefault();
    	
    	var tests = $(".webTests input[type='checkbox']:checked");
    	var envs = $(".wtEnvs input[type='checkbox']:checked");
    	var sites = $(".wtSites input[type='checkbox']:checked");
    	var scriptName = $("#tScriptName").val().trim();
    	var pass = WebTestValidation(tests, envs, sites, scriptName)
    	if (!pass)
    	    return;

    	wTests = [];
    	for (var i = 0; i < tests.length; i++) 
    		wTests.push($(tests[i]).attr("value") + "::" + $(tests[i]).attr("name"));
    	wEnvs = [];
    	for (var j = 0; j < envs.length; j++) 
    		wEnvs.push($(envs[j]).attr("value"));
    	wSites = [];
    	for (var k = 0; k < sites.length; k++) 
    		wSites.push($(sites[k]).attr("value"));

    	var data = JSON.stringify({ ScriptName: scriptName, TestCalls: wTests, EnvironmentIDs: wEnvs, SiteIDs: wSites });
		CallTestWS("CreateWebTestScript", data, RunGenSuccess, TestError);
    });

    //form validation
    function WebTestValidation(tests, envs, sites, scriptName) {
        $(".error").hide();
        if (envs.length == 0) {
            $(".error").html(errNoEnvs).show();
            return false;
        } else if (sites.length == 0) {
            $(".error").html(errNoSites).show();
            return false;
        } else if (tests.length == 0) {
            $(".error").html(errNoTests).show();
            return false;
        } else if(scriptName == "") {
            $(".error").html(errNoScriptName).show();
            return false;
        }
        return true;
    }

	//	SHARED TEST FUNCTIONS //////////

    function TestError(e) {
    	$(".resultSet").append(e);
    }

	//unit test result callback
    function RunGenSuccess(data, status) {
    	var css = (data.d.Success) ? "pass" : "fail";
    	$(".resultSet").append("<div class='result corners " + css + "'><div class='ind corners'></div><div class='rResult'>" + data.d.Message + "</div></div>");
    }

    $(".genToggle").click(function (e) {
    	var text = $(this).text();
    	if (text.indexOf("+") == -1) {
    		$(this).text("+");
    		$(".generate .submit").hide();
    		$(".genFields").slideUp();
    	} else {
    		$(this).text("-");
    		$(".genFields").slideDown();
    		setTimeout(function () { $(".generate .submit").show(); }, 250);
		}
    });

	function CallTestWS(WebServiceMethod, callData, SuccessHandler, ErrorHandler) {
		$.ajax({
			type: "POST",
			url: "/sitecore modules/Web/teststar/service/testservice.asmx/"+ WebServiceMethod,
			data: callData,
			contentType: "application/json",
			dataType: "json",
			success: SuccessHandler,
			error: ErrorHandler
		});
	}

	function TestsCompleted() {
	    $(".resultCounter").text("100%");
	    var sCount = $(".resultSet .Success").length;
	    var fCount = $(".resultSet .Failure").length;
	    var eCount = $(".resultSet .Error").length;
	    var skCount = $(".resultSet .Skipped").length;
	    $(".resultStatus").html("<div class='blueBox corners'>" + sCount + " " + labSuccess + ", " + fCount + " " + labFailure + ", " + eCount + " " + labError + ", " + skCount + " " + labSkipped + "</div>");
	}

	var checkAllTests = true;
	$(".allSelector").html("(" + selectTests + ")");
	$(".allSelector").click(function () {
	    var testBoxes = $(".testList input[type='checkbox']");
	    if ($(this).html().indexOf(deselectTests) >= 0) {
	        $(this).html("(" + selectTests + ")");
	        testBoxes.prop('checked', false);
	    } else {
	        $(this).html("(" + deselectTests + ")");
	        testBoxes.prop('checked', true);
	    }
	});
});