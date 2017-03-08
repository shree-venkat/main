(function () {
    "use strict";

    function routeProviders($stateProvider, $urlRouterProvider) {
        var providers = {
            $stateProvider: $stateProvider,
            $urlRouterProvider: $urlRouterProvider
        };

        this.$get = function () {
            return {
                config: providers
            };
        };
    }

    routeProviders.$inject = ["$stateProvider", "$urlRouterProvider"];

    angular
        .module("app.common.router")
        .provider("routeProviders", routeProviders);
})();