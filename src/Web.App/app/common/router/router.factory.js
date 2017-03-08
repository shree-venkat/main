(function () {
    "use strict";

    function routeRecorder($rootScope, $state, routeProviders) {
        var $stateProvider = routeProviders.config.$stateProvider;
        var $urlRouterProvider = routeProviders.config.$urlRouterProvider;
        var hasOtherwise = false;

        function configureStates(states, otherwisePath) {
            states.forEach(function (state) {
                $stateProvider.state(state.state, state.config);
            });

            if (otherwisePath && !hasOtherwise) {
                hasOtherwise = true;
                $urlRouterProvider.otherwise(otherwisePath);
            }
        }

        var service = {
            save: configureStates
        };

        return service;
    }

    routeRecorder.$inject = ["$rootScope", "$state", "routeProviders"];

    angular
        .module("app.common.router")
        .factory("routeRecorder", routeRecorder);
})();