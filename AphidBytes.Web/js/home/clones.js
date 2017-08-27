(function ($) {
    $("#divProcessing").hide();
    $("form").submit(function (event) {
        $("#divProcessing").show();
    });
})(window.jQuery);