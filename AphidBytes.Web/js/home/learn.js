(function ($) {
    //$('.parallax-window').parallax({
    //    naturalWidth: 1280,
    //    naturalHeight: 720
    //});

    $(".background-video").vide("/videos/register-main.mp4", {
        volume: 1,
        playbackRate: 1,
        muted: true,
        loop: true,
        autoplay: true,
        position: '50% 50%', // Similar to the CSS `background-position` property.
        posterType: 'detect', // Poster image type. "detect" — auto-detection; "none" — no poster; "jpg", "png", "gif",... - extensions.
        resizing: true, // Auto-resizing, read: https://github.com/VodkaBears/Vide#resizing
        bgColor: 'transparent', // Allow custom background-color for Vide div,
        className: '' // Add custom CSS class to Vide div
    });

    
})(window.jQuery);