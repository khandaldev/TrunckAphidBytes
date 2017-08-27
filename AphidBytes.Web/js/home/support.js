(function ($) {
    $(".link-section").on("click", function (e) {
        e.preventDefault();
        $(".link-section").removeClass("selected");
        var sectionId = $(e.currentTarget).attr("data-section");
        $(e.currentTarget).addClass("selected");
        (sectionId === "clones")
            ? $(".clones").show()
            : $(".clones").hide();
        (sectionId === "accounts")
            ? $(".accounts").show()
            : $(".accounts").hide();
        (sectionId === "advertising")
            ? $(".advertising").show()
            : $(".advertising").hide();
        (sectionId === "video-tutorials")
            ? $(".video-tutorials").show()
            : $(".video-tutorials").hide();
    });

    $(".send-feedback").on("click", function (e) {
        e.preventDefault();
        var email = $("#emailid").val().trim();
        var text = $("#feedbacktext").val().trim();
        var subject = $("#subject").val().trim();
        $.post('/FeedBack/FeedBackRegister', { Email: email, Text: text, Subject: subject }, function (data) {
            if (data == true) {
                $("#Successpopup").show();
            } else {
                alert(data);
                return false;
            }
        });
    });

    $(".close-popup").on("click", function (e) {
        e.preventDefault();
        $("#Successpopup").hide();
    });

})(window.jQuery);