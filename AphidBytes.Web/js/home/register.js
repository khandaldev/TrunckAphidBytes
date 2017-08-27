(function ($) {

   

    $('#chkagree').change(function () {
        if ($(this).is(":checked")) {
                $('#btnSendPayment').prop('disabled', false);
        }
        else {
            $('#btnSendPayment').prop('disabled', true);
        }

    });

    $('#txt-username').keypress(function () {
        
        $("#UserNameDiv").addClass('hidden');
        $("#UserNameDiv").removeClass('alert alert-danger');
    });
    $('#txt-username').keyup(function () {
        
        $("#UserNameDiv").addClass('hidden');
        $("#UserNameDiv").removeClass('alert alert-danger');
    });
    $('#txt-emailaddress').keypress(function () {        
        $("#EmailDiv").addClass('hidden');
        $("#EmailDiv").removeClass('alert alert-danger');
    });
    $('#txt-emailaddress').keyup(function () {
        
        $("#EmailDiv").addClass('hidden');
        $("#EmailDiv").removeClass('alert alert-danger');
    });


    
    


    $(".close-button").on("click", function (e) {
        e.preventDefault();
        $(".comparison-chart").hide();
        $(".main-content, .inner-navigation, .account-comparison-button").show();
    });

    $(".account-comparison-button").on("click", function (e) {
        e.preventDefault();
        $(".comparison-chart").show();
        $(".main-content, .inner-navigation, .account-comparison-button").hide();
    });

    $(".promotion-validation").on("change", function (e) {
        $.post("/api/validatepromotion", { promoCode: $(this).val() }, function (data) {
            if (data == "valid") {
                $(".promotion-validation").closest(".form-group").removeClass("has-error");
            }
            else {
                $(".promotion-validation").closest(".form-group").removeClass("has-error").addClass("has-error");
            }
        });
    });

    var pageIndex = 0;
    var pageOrder = [".navigate-to-byter", ".navigate-to-basic", ".navigate-to-premium", ".navigate-to-aphidlabs"];

    function setNavigation() {
        $(pageOrder.join(", ")).removeClass("selected");
        $(pageOrder[pageIndex]).addClass("selected");
        if (pageIndex < (pageOrder.length - 1)) {
            $(".navigate-right").show();
        }
        else {
            $(".navigate-right").hide();
        }
        if (pageIndex > 0) {
            $(".navigate-left").show();
        }
        else {
            $(".navigate-left").hide();
        }
    }

    $(".navigate-to-byter").on("click", function (e) {
        e.preventDefault();
        pageIndex = 0;
        $(".basic-content, .premium-content, .aphidlabs-content").hide();
        $(".byter-content, .button-content").show();
        setNavigation();
    });

    $(".navigate-to-basic").on("click", function (e) {
        e.preventDefault();
        pageIndex = 1;
        $(".byter-content, .premium-content, .aphidlabs-content").hide();
        $(".basic-content, .button-content").show();
        setNavigation();
    });

    $(".navigate-to-premium").on("click", function (e) {
        e.preventDefault();
        pageIndex = 2;
        $(".byter-content, .basic-content, .aphidlabs-content, .button-content").hide();
        $(".premium-content").show();
        setNavigation();
    });

    $(".navigate-to-aphidlabs").on("click", function (e) {
        e.preventDefault();
        pageIndex = 3;
        $(".byter-content, .basic-content, .premium-content").hide();
        $(".aphidlabs-content, .button-content").show();
        setNavigation();
    });

    $(".navigate-left").on("click", function (e) {
        e.preventDefault();
        pageIndex--;
        $(pageOrder[pageIndex]).click();
    });

    $(".navigate-right").on("click", function (e) {
        e.preventDefault();
        pageIndex++;
        $(pageOrder[pageIndex]).click();
    });

    $(".btn-facebook").on("click", function (e) {
        e.preventDefault();
        window.location.href = window.aphidByte.services.signUpFacebook + "?id=" + (pageIndex + 1);
    });

    $(".btn-googleplus").on("click", function (e) {
        e.preventDefault();
        debugger;
        window.location.href = window.aphidByte.services.signUpGooglePlus + "?id=" + (pageIndex + 1);
    });

    $(".btn-email").on("click", function (e) {
        e.preventDefault();
        switch (pageIndex) {
            case 0:
                window.location.href = window.aphidByte.services.signUpEmailByter;
                break;
            case 1:
                window.location.href = window.aphidByte.services.signUpEmailBasic;
                break;
            case 2:
                window.location.href = window.aphidByte.services.signUpEmailPremium;
                break;
            default:
                window.location.href = window.aphidByte.services.signUpEmailAphidLab;
                break;
        }
    });

    $(".password-match").on("change", function (e) {
        var allPasswordsMatch = false;
        var lastPassword = "";
        $(".password-match").each(function () {
            var $elem = $(this);
            if (lastPassword != "" && lastPassword == $elem.val()) {
                $(".password-match").closest(".form-group").removeClass("has-error");
            }
            else {
                $(".password-match").closest(".form-group").removeClass("has-error").addClass("has-error");
            }
            lastPassword = $(this).val();
        });
    });

    var currYear = new Date().getFullYear();
    var prevYear = currYear - 100;
    $(".date-picker").datepicker({
        changeMonth: true,
        changeYear: true,
        yearRange: prevYear + ":" + currYear
    });

    $(".addplan").on("click", function (e) {
        e.preventDefault();
        var $el = $(this),
            $elUrl = $el.attr("href"),
            $elDataPlan = $el.data('planid');

        $.post($elUrl, { planId: $elDataPlan }, function (data) {
            //TODO: create a success message on the page.
            if (data == "Success") {
                location.reload(true);
            }
        });
    });

   
})(window.jQuery);


