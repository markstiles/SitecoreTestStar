$(document).ready(function () {
    
    //UNIT TESTING 

    //unit test submit
	var catList;
	$("#utSubmit").click(function (e) {
	    $(".resultSet").html("");

	    e.preventDefault();
		catList = [];
		$(".utCategories input[type='checkbox']").each(function (key, value) {
			if (!$(this).is(':checked'))
				return;
			catList.push($(value).attr("name"));
		});
		//run test through web service
		var data = JSON.stringify({ Categories: catList });
		CallTestWS("RunUnitTests", data, RunUnitTestSuccess);
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

        for (var i = 0; i < tests.length; i++) {
            var curTest = tests[i];
            for (var j = 0; j < envs.length; j++) {
                var curEnv = envs[j];
                for (var k = 0; k < sites.length; k++) {
                    var curSite = sites[k];
                    //call web service
                    var data = JSON.stringify({ EnvironmentID: $(curEnv).attr("value"), SiteID: $(curSite).attr("value"), AssemblyName: $(curTest).attr("value"), ClassName: $(curTest).attr("name") });
                    CallTestWS("RunWebTest", data, RunWebTestSuccess);
                }
            }
        }
	});

    //web test result callback
    function RunWebTestSuccess(data, status) {
        data.d.forEach(function (res) {
            var css = (res.Flag) ? "even" : "odd";
            var s = "<div class='result corners " + css + " " + res.Type + "'><div class='ind corners'></div>";
            s += "<div class='rSite'>" + res.Site + " - " + res.Environment + "</div>";
            s += "<div class='clearfix'></div>";
            if (res.Name != "")
                s += "<div class='rMethod'>" + res.Method + "</div>";
            var div = (res.Value.Length > 0) ? ": " : "";
            s += "<div class='rResult'>" + res.Name + div + " " + res.Value + "</div>";
            s += "</div>";

            $(".resultSet").append(s);
        });
    }

    //SHARED FUNCTIONS

	function CallTestWS(WebServiceMethod, callData, SuccessHandler) {
		$.ajax({
			type: "POST",
			url: "/sitecore modules/Web/teststar/service/testservice.asmx/"+ WebServiceMethod,
			data: callData,
			contentType: "application/json",
			dataType: "json",
			success: SuccessHandler,
			error: function (e) { }
		});
	}
});