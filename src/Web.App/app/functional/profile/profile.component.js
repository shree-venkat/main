(function () {
    "use strict";

    function profileCtrl($rootScope, profileResource, logger) {
        $rootScope.bannerTitle = "Shree Venkat";
        $rootScope.bannerSubtitle = "A look at my contract experience";

        var vm = this;

        var initProfile = function () {
            vm.tabs = ['Clientele', 'Proficiency'];//Qualifications
            vm.profile = {};
            vm.activeTab = vm.tabs[0];
            vm.searchSkill = '';
            vm.orderProps = ['categoryId', '-rating', 'skill'];

            $rootScope.showLoadingMessage = "Loading files ...";
            profileResource.get().$promise.then(function (data) {
                vm.profile = data;
                vm.profile.experience = vm.profile.experience.sort(function(a, b) {
                    return a.startDate < b.startDate;
                });
                vm.profile.loadedClient = vm.profile.experience[0].client;
            }, function (response) {
                logger.error("Error", "Sorry, an error to load my profile", response.data);
            }).finally(function () {
                $rootScope.showLoadingMessage = false;
            });
        };

        vm.initProfile = initProfile;

        vm.initProfile();
    }

    profileCtrl.$inject = ["$rootScope", "profileResource", "logger"];

    angular
        .module('app.profile')
        .component('profile', {
            restrict: 'E',
            scope: {},
            templateUrl: "app/functional/profile/profile.template.html",
            controller: profileCtrl,
            controllerAs: "vm" // OR use $ctrl in HTML template
        });
})();