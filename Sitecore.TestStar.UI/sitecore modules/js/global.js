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
	$("#utSubmit").click(function (e) {
	    $(".resultSet").html("");
	    e.preventDefault();

	    uTests = [];
		uPos = 0;
	    var checkedBoxes = $(".testInputs input[type=checkbox]:checked").each(function (key, value) {
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
			$(".resultCounter").text("100%");
			return;
		} else {
			var per = (uPos / uTests.length) * 100;
			$(".resultCounter").text(Math.floor(per) + "%");
		}

		CallTestWS("RunUnitTests", uTests[uPos], RunUnitTestSuccess, TestError);
		uPos++;
	}

	//test generation
	$("#utGenerate").click(function (e) {
		$(".resultSet").html("");
		e.preventDefault();

		var tCalls = [];
		var checkedBoxes = $(".testInputs input[type=checkbox]:checked").each(function (key, value) {
			tCalls.push($(value).attr("value") + "::" + $(value).attr("name"));
		});

		var data = JSON.stringify({ ScriptName: $("#utScriptName").val(), TestCalls: tCalls });

		CallTestWS("CreateUnitTestScript", data, RunGenSuccess, TestError);
	});
		
	//	WEB TESTING //////////

    //if system is (un)checked then (un)check all corresponding sites
	$(".wtSystems input[type='checkbox']").click(function (e) {
	    var type = $(this).attr("name");
	    if ($(this).is(':checked'))
	        $(".wtSites input." + type).prop('checked', true);
	    else
	        $(".wtSites input." + type).prop('checked', false);
	});

    //web test submit
    $("#wtSubmit").click(function (e) {
        $(".resultSet").html("");

        e.preventDefault();
        $(".error").hide();
        //form validation
        var tests = $(".wtTests input[type='checkbox']:checked");
        var envs = $(".wtEnvs input[type='checkbox']:checked");
        var sites = $(".wtSites input[type='checkbox']:checked");
        if (tests.length == 0 || envs.length == 0 || sites.length == 0) {
	        $(".error").show();
	        if (envs.length == 0)
	            $(".error").html("You should select at least one environment");
	        else if (sites.length == 0)
	            $(".error").html("You should select at least one site");
	        else if (tests.length == 0)
	            $(".error").html("You should select at least one test");
	    }

        wTests = [];
        wPos = 0;
        for (var i = 0; i < tests.length; i++) {
            var curTest = tests[i];
            for (var j = 0; j < envs.length; j++) {
                var curEnv = envs[j];
                for (var k = 0; k < sites.length; k++) {
                    var curSite = sites[k];
                    //call web service
                    var data = JSON.stringify({ EnvironmentID: $(curEnv).attr("value"), SiteID: $(curSite).attr("value"), AssemblyName: $(curTest).attr("value"), ClassName: $(curTest).attr("name") });
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
            console.log(res.Message.length);
            console.log(res.Message);
            s += "<div class='rResult'>" + res.Type + div + " " + res.Message + "</div>";
            s += "</div>";

            $(".resultSet").prepend(s);
        });

        WebTestRunner();
    }

	//makes recursive calls while tests are available
    function WebTestRunner() {
    	if (wPos >= wTests.length) {
    		$(".resultCounter").text("100%");
    		return;
    	} else {
    		var per = (wPos / wTests.length) * 100;
    		$(".resultCounter").text(Math.floor(per) + "%");
    	}

    	CallTestWS("RunWebTest", wTests[wPos], RunWebTestSuccess, TestError);
    	wPos++;
    }

	//test generation
    $("#wtGenerate").click(function (e) {
    	$(".resultSet").html("");
    	e.preventDefault();

    	//form validation
    	var tests = $(".wtTests input[type='checkbox']:checked");
    	var envs = $(".wtEnvs input[type='checkbox']:checked");
    	var sites = $(".wtSites input[type='checkbox']:checked");
    	if (tests.length == 0 || envs.length == 0 || sites.length == 0) {
    		$(".error").show();
    		if (envs.length == 0)
    			$(".error").html("You should select at least one environment");
    		else if (sites.length == 0)
    			$(".error").html("You should select at least one site");
    		else if (tests.length == 0)
    			$(".error").html("You should select at least one test");
    	}

    	wTests = [];
    	for (var i = 0; i < tests.length; i++) 
    		wTests.push($(tests[i]).attr("value") + "::" + $(tests[i]).attr("name"));
    	wEnvs = [];
    	for (var j = 0; j < envs.length; j++) 
    		wEnvs.push($(envs[j]).attr("value"));
    	wSites = [];
    	for (var k = 0; k < sites.length; k++) 
    		wSites.push($(sites[k]).attr("value"));

    	var data = JSON.stringify({ ScriptName: $("#wtScriptName").val(), TestCalls: wTests, EnvironmentIDs: wEnvs, SiteIDs: wSites });
		CallTestWS("CreateWebTestScript", data, RunGenSuccess, TestError);
    });

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
});