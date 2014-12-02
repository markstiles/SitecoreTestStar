$(document).ready(function () {
    
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
		CallTestWS("RunUnitTests", data, RunTestSuccess);
	});

	function RunTestSuccess(data, status) {
		$(".resultSet").html("");
		data.d.forEach(function (cat) {
			var css = (cat.Flag) ? "even" : "odd";
			var s = "<div class='result " + css + " " + cat.Type + "'>";
			if (cat.Name != "")
				s += "<div class='rMethod'>" + cat.Method + " -</div>";
			var div = (cat.Value.Length > 0) ? ": " : "";
			s += "<div class='rResult'>" + cat.Name + div + " " + cat.Value + "</div>";
			s += "</div>";

			$(".resultSet").append(s);
		});
	}

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