$(document).ready(function () {
    
    $(".cblSystems input").click(function (e) {
        var type = $(this).val();
        //debug
        //console.log("type:" + type);
        //console.log(".cblSites ." + type + " input");
        if ($(this).is(':checked'))
            $(".cblSites ." + type + " input").prop('checked', true);
        else 
            $(".cblSites ." + type + " input").prop('checked', false);
    });

    $(".testSubmit, .generateSubmit").click(function (e) {
        var testSelected = $(".cblTests input").is(':checked');
        var envSelected = $(".cblEnv input").is(':checked');
        var siteSelected = $(".cblSites input").is(':checked');
        //debug
        //e.preventDefault();
        //console.log("test selected:" + testSelected);
        //console.log("env selected:" + envSelected);
        //console.log("site selected:" + siteSelected);
        if (!testSelected || !envSelected || !siteSelected) {
            e.preventDefault();
            if (!testSelected)
                $(".error").html("You should select at least one test");
            else if (!envSelected)
                $(".error").html("You should select at least one environment");
            else if (!siteSelected)
                $(".error").html("You should select at least one site");
        } 
    });

});