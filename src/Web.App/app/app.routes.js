(function () {
    "use strict";

    function getStates() {
        return [
			{
			    state: "home",
			    config: {
			        pageTitle: "Home",
			        bannerTemplate: "app/functional/home/banner-home.html",
			        url: "/",
			        template: "<home></home>"
			    }
			},
			{
			    state: "home-no-slash",
			    config: {
			        pageTitle: "Home",
			        bannerTemplate: "app/functional/home/banner-home.html",
			        url: "",
			        template: "<home></home>"
			    }
			},
			{
			    state: "error",
			    config: {
			        pageTitle: "Error",
			        bannerTemplate: "banner-error.html",
			        url: "/error",
			        templateUrl: "app/functional/static-pages/error.html"
			    }
			}
        ];
    }

    function routes(routeRecorder) {
        routeRecorder.save(getStates(), "/error");
    }

    routes.$inject = ["routeRecorder"];

    angular
        .module("app")
        .run(routes);
})();
