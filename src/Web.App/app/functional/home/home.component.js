(function () {
    "use strict";

    function homeCtrl($rootScope, homeResource, logger) {
        var vm = this;

        var initForm = function () {
            vm.name = '';
            vm.email = '';
            vm.copyUser = false;
            vm.message = '';
        };

        var sendMessage = function (valid) {
            if (!valid) {
                logger.error("Error", "Invalid form.");
                return;
            }

            $rootScope.showLoadingMessage = "Sending email ...";
            homeResource.sendEmail({ fromName: vm.name, fromEmail: vm.email, ccUser: vm.copyUser, message: vm.message }).$promise
                .then(function () {
                    vm.initForm();
                    logger.success("Success", "Message successfully sent.");
                }, function (response) {
                    logger.error("Error", "Unable to retrieve login options", response.data);
                }).finally(function () {
                    $rootScope.showLoadingMessage = false;
                });
        };

        vm.initForm = initForm;
        vm.sendMessage = sendMessage;

        vm.initForm();
    }

    homeCtrl.$inject = ["$rootScope", "homeResource", "logger"];

    angular
        .module('app.home')
        .component('home', {
            restrict: 'E',
            scope: {},
            templateUrl: "app/functional/home/home.template.html",
            controller: homeCtrl,
            controllerAs: "vm" // OR use $ctrl in HTML template
        });
})();