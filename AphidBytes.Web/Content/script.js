/* 
	author: istockphp.com
*/
jQuery(function($) {
    
    $("a.topopup").click(function () {
        $.post('/Home/Fetch_Ad_Video_Data', { ad_type_id: 2 }, function (data) {
            if (data != null) {
                var vid_url = data.AdVideo; ///Basic Audio Files/4.mp4';  //1.mp4  2.MPG  3.wmv  4.mpg
                var vid_description = data.AdInformation; //"place content here for description...";
                var img_url = data.AdPicture;
                var ad_url = data.AdHyperLinkUrl;
                var ad_title = data.Title;
                var ad_type_id = data.AdTypeID;
                var credits = data.CreditsID;
                var surveyid = data.surveyid;
                if (ad_type_id == 2) {
                    if (vid_url != null) {
                        $('#ad_video_tag').attr('src', vid_url);
                        $('#ad_desc').text(vid_description);
                        $('#ad_title').text(ad_title);
                        $('#survey').attr('placeholder', surveyid);

                        document.getElementById("ad_website").href = "http://" + ad_url;

                        switch (credits) {
                            case 1: $('#ad_creadits').text("1 Credit");
                                break;
                            case 2: $('#ad_creadits').text("2 Credits");
                                break;
                            case 3: $('#ad_creadits').text("3 Credits");
                                break;
                        }

                    } else {
                        if (img_url != null) {
                        }

                    }
                }
            }
            else {
                $('#ad_video_tag').attr('style', 'display:none;');
                $('#ad_website').attr('style', 'display:none;');
                $('#ad_creadits').attr('style', 'display:none;');
                $('#takesurvey').attr('style', 'display:none;');
            }
        });
			loading(); // loading
			setTimeout(function(){ // then show popup, deley in .5 second
				loadPopup(); // function show popup 
			}, 500); // .5 second
	return false;
	});
	
	/* event for close the popup */
	$("div.close").hover(
					function() {
						$('span.ecs_tooltip').show();
					},
					function () {
    					$('span.ecs_tooltip').hide();
  					}
				);
	
	$("div.close").click(function() {
	    disablePopup();   // function close pop up
	    var ele = document.getElementById("ad_video_tag");
	    ele.pause();
	});
	
	$(this).keyup(function(event) {
		if (event.which == 27) { // 27 is 'Ecs' in the keyboard
			disablePopup();  // function close pop up
		}  	
	});
	
	$("div#backgroundPopup").click(function() {
		disablePopup();  // function close pop up
	});
	
	$('a.livebox').click(function() {
		alert('Hello World!');
	return false;
	});
	

	 /************** start: functions. **************/
	function loading() {
		$("div.loader").show();  
	}
	function closeloading() {
		$("div.loader").fadeOut('normal');  
	}
	
	var popupStatus = 0; // set value
	
	function loadPopup() { 
		if(popupStatus == 0) { // if value is 0, show popup
			closeloading(); // fadeout loading
			$("#toPopup").fadeIn(0500); // fadein popup div
			$("#backgroundPopup").css("opacity", "0.7"); // css opacity, supports IE7, IE8
			$("#backgroundPopup").fadeIn(0001); 
			popupStatus = 1; // and set value to 1
		}	
	}
		
	function disablePopup() {
		if(popupStatus == 1) { // if value is 1, close popup
			$("#toPopup").fadeOut("normal");  
			$("#backgroundPopup").fadeOut("normal");  
			popupStatus = 0;  // and set value to 0
		}
	}
	/************** end: functions. **************/
}); // jQuery End