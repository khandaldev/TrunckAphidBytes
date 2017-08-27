(function ($) {
    $(".btn-radio").on("click", function (e) {
        e.preventDefault();
        $(".btn-radio").removeClass("selected");
        $(e.currentTarget).addClass("selected");
        var newPlaceholder = $(e.currentTarget).attr("data-placeholder");
        $("#Searchtext").attr("placeholder", newPlaceholder);
    });

    $(".search-button").click(function () {
        var textToSearch = $("#Searchtext").val();
        var catToSearch = $(".btn-radio.selected").attr("data-category");
        $.post('/Home/Searching', { Texttosearch: '%' + textToSearch + '%', category: catToSearch, tracking: textToSearch }, function (data) {
            window.location = data;
        })
    })
})(window.jQuery);