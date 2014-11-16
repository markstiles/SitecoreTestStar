$(document).ready(function () {
    
	$("#utSubmit").click(function (e) {
		e.preventDefault();
		$(".utCategories input[type='checkbox']").each(function (key, value) {
			if (!$(this).is(':checked'))
				return;
			//run test through web service
			console.log(value);
		});
	});

	$(".utSuite").click(function (e) {
    	e.preventDefault();
		var testName = $(this).attr("test");
		$.ajax({
			type: "POST",
			url: "/sitecore modules/Web/teststar/service/testservice.asmx/GetCategories",
			data: "{ 'TestSuiteName':'" + testName + "'}",
			contentType: "application/json",
			dataType: "json",
			success: function (data, status) {
				var a = ["a", "b", "c"];
				data.d.forEach(function (cat) {
					$(".utCategories").append('<div class="row"><input type="checkbox" name="' + cat + '" value="' + cat + '"><label for="id' + cat + '">' + cat + '</label></div>');
				});
			},
			error: function (e) { }
		});
	});
});