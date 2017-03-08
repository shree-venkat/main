(function () {
    "use strict";

    function navbarCtrl($http, $sce, logger) {
        var vm = this;
        vm.navbarExpanded = false;
        vm.user = null;
        vm.loginMenuHtml = null;

        var loadLoginOptions = function () {
            $http.get("Account/LoginMenu").then(function (response) {
                vm.loginMenuHtml = $sce.trustAsHtml(response.data);
            }, function (response) {
                logger.error("Error", "Unable to retrieve login options", response.data);
            });
        };

        vm.loadLoginOptions = loadLoginOptions;

        vm.loadLoginOptions();
    }

    navbarCtrl.$inject = ["$http", "$sce", "logger"];

    var navbarModule = angular.module('app.navbar', []);
    navbarModule.controller("navbarCtrl", navbarCtrl);
})();