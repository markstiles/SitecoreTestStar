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

    //UNIT TESTING 

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
			if (res.Name != "")
				s += "<div class='rMethod'>" + res.Method + "</div>";
			var div = (res.Value.Length > 0) ? ": " : "";
			s += "<div class='rResult'>" + res.Name + div + " " + res.Value + "</div>";
			s += "</div>";

			$(".resultSet").append(s);
		});

		UnitTestRunner();
	}

	function UnitTestRunner() {
		if (uPos >= uTests.length)
			return;

		CallTestWS("RunUnitTests", uTests[uPos], RunUnitTestSuccess, TestError);
		uPos++;
	}

    //WEB TESTING

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
            var div = (res.Value.Length > 0) ? ": " : "";
            s += "<div class='rResult'>" + res.Name + div + " " + res.Value + "</div>";
            s += "</div>";

            $(".resultSet").append(s);
        });

        WebTestRunner();
    }

    function WebTestRunner() {
    	if (wPos >= wTests.length)
    		return;

    	CallTestWS("RunWebTest", wTests[wPos], RunWebTestSuccess, TestError);
    	wPos++;
    }

    function TestError(e) {
    	$(".resultSet").append(e);
    }

    //SHARED FUNCTIONS

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