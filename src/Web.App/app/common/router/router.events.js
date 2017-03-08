(function () {
    "use strict";

    function routeEvents($rootScope, $state, logger) {
        var errorHandled = false;

        function handleStateChangeEvents() {
            $rootScope.$on("$stateChangeStart", function () {
                /* Reset global variables */
                $rootScope.bannerTitle = "";
                $rootScope.bannerSubtitle = "";
                $rootScope.customBannerHtml = "";
                $rootScope.showLoadingMessage = false;
                $rootScope.bannerContentSrc = "";
                $rootScope.currentState = "";
            });

            $rootScope.$on("$stateChangeSuccess", function (event, toState) {
                $rootScope.pageTitle = toState.pageTitle;
                $rootScope.bannerContentSrc = toState.bannerTemplate;
                $rootScope.currentState = toState.name;
            });

            $rootScope.$on("$stateChangeError", function (event, toState, toParams, fromState, fromParams, error) {
                if (!errorHandled) {
                    errorHandled = true;
                    logger.warning("Warning", error);
                    //$state.go("home");
                }
            });
        }

        handleStateChangeEvents();
    }

    routeEvents.$inject = ["$rootScope", "$state", "logger"];

    angular
        .module("app.common.router")
        .run(routeEvents);
})();