$(document).ready(function () {
    
    //UNIT TESTING 

    //unit test submit
	var catList;
	$("#utSubmit").click(function (e) {
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
		$(".resultSet").html("");
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
        e.preventDefault();
        $(".error").hide();
        //form validation
        var testSelected = $(".wtTests input").is(':checked');
	    var envSelected = $(".wtEnvs input").is(':checked');
	    var siteSelected = $(".wtSites input").is(':checked');
	    if (!testSelected || !envSelected || !siteSelected) {
	        $(".error").show();
	        if (!envSelected)
	            $(".error").html("You should select at least one environment");
	        else if (!siteSelected)
	            $(".error").html("You should select at least one site");
	        else if (!testSelected)
	            $(".error").html("You should select at least one test");
	    }

        //foreach test
            //foreach env
                //foreach site
         
        //run test through web service
	    var data = JSON.stringify({ EnvironmentID: e, SiteID: s, AssemblyName: an, ClassName: cn });
	    CallTestWS("RunWebTest", data, RunWebTestSuccess);
	});

    //web test result callback
    function RunUnitTestSuccess(data, status) {
        $(".resultSet").html("");
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