(function () {
    "use strict";

    $(".dropdown.hover").hover(function () {
        $(this).find(".dropdown-menu").first().attr("style", "display: block");
    }, function () {
        $(this).find(".dropdown-menu").first().attr("style", "display: none");
    });

    var ping = function(page) {
        $.ajax({
            type:"GET",
            url: page,
            dataType:"html",
            success:function() {
                // do nothing
            },
            error:function(response) {
                window.console.log("Ping failed for " + page + ": " + response);
            }
        });
    };

    var keepAlive = function() {
        var now = new Date();
        var dayOfWeek = now.getDay();
        var timeOfDay = now.getHours();
        if (dayOfWeek < 1 || dayOfWeek > 5 || timeOfDay < 8 || timeOfDay > 18) {
            return; // Not business hours - Mon ~ Fri 8AM ~ 6PM
        }

        var webpages = [ 
            "http://www.shreevenkat.co.uk/ver",
            "http://api.shreevenkat.co.uk/" ];

        for (var i = 0; i < webpages.length; i++) {
            ping(webpages[i]);
        }
    };

    window.setInterval(keepAlive, 2.5 * 60 * 1000);

    /*$("#carousel-generic").on("slide.bs.carousel", function () {
        var bannerSection = $(".page-banner-section").first();
        var banner = $(".page-banner-section .page-banner").first();
        if ($(bannerSection).hasClass("color-primary")) {
            $(bannerSection).removeClass("color-primary");
            $(banner).removeClass("color-primary");
            $(bannerSection).addClass("color-2");
            $(banner).addClass("color-2");
        }
        else if ($(bannerSection).hasClass("color-2")) {
            $(bannerSection).removeClass("color-2");
            $(banner).removeClass("color-2");
            $(bannerSection).addClass("color-3");
            $(banner).addClass("color-3");
        }
        else if ($(bannerSection).hasClass("color-3")) {
            $(bannerSection).removeClass("color-3");
            $(banner).removeClass("color-3");
            $(bannerSection).addClass("color-primary");
            $(banner).addClass("color-primary");
        }
        else if ($(bannerSection).hasClass("color-4")) {
                $(bannerSection).removeClass("color-4");
                $(banner).removeClass("color-4");
                $(bannerSection).addClass("color-primary");
                $(banner).addClass("color-primary");
            }
    });*/

    /*var bannerHeight = $(".page-banner-section").height() + 12;
    $(".body-and-footer").attr("style", "min-height: calc(100% - " + bannerHeight + "px)");*/
})();
