(function () {
    'use strict';

    function adminsController($rootScope, $location, userResource, logger, $state, $uibModal) {
        $rootScope.bannerTitle = "Website Administration";
        $rootScope.bannerSubtitle = "Managing accounts using AngularJS";

        var vm = this;
        vm.page = ($location.search().page == null ? 1 : $location.search().page);
        vm.pageSize = ($location.search().pageSize == null ? "10" : $location.search().pageSize);
        vm.searchTerm = ($location.search().searchTerm == null ? '' : $location.search().searchTerm);
        vm.totalPages = 0;
        vm.total = 0;

        vm.admins = [];

        var loadUsers = function () {
            userResource.query({ page: vm.page, pageSize: vm.pageSize, searchTerm: vm.searchTerm }).$promise
                .then(function (data) {
                    vm.admins = data.Items;
                    vm.totalPages = data.TotalPages;
                    vm.total = data.TotalCount;
                }).catch(function (response) {
                    logger.error("Error", "Unable to retrieve admis", response.data);
                });
        };

        vm.loadUsers = loadUsers;

        vm.isAdmin = function (user) {
            if (user.Roles.indexOf('Administrator') >= 0) {
                return true;
            } else {
                return false;
            }
        };

        vm.search = function (searchTerm) {
            vm.searchTerm = searchTerm;
            vm.loadUsers();
        };

        vm.pageSizeChanged = function () {
            vm.page = 1;
            vm.loadUsers();
        };

        vm.pageChanged = function () {
            vm.loadUsers();
        };

        vm.addUser = function () {
            vm.modalInstance = $uibModal.open({
                templateUrl: 'app/functional/administrators/createAdminModal.html?bust=' + Math.random().toString(36).slice(2),
                controller: 'createAdminController',
                controllerAs: 'vm',
                size: 'md'
            });

            vm.modalInstance.result.then(function () {
                    vm.loadUsers();
                }).catch(function () {
                });
        };

        vm.deleteUser = function (user) {
            var result = false;
            var message = "Are you sure you want to remove " + user.DisplayName + " as Administrator?";
            vm.modalInstance = $uibModal.open({
                templateUrl: 'app/shared/confirmModal.html?bust=' + Math.random().toString(36).slice(2),
                controller: 'confirmController',
                controllerAs: 'vm',
                resolve: {
                    confirmTitle: function () { return "Confirm"; },
                    confirmMessage: function () { return message; },
                    confirmOkName: function () { return "Yes"; },
                    confirmCancelName: function () { return "No"; }
                }
            });

            vm.modalInstance.result.then(function (response) {
                result = response;

                if (result) {
                    userResource.delete({ username: user.Username }).$promise.then(function () {
                        vm.loadUsers();
                        logger.success("Success", user.DisplayName + " successfully deleted");
                    }).catch(function (response) {
                        logger.error("Error", "Unable to delete user", response.data);
                    });
                }
            }).catch(function () {
            });
        };

        vm.loadUsers();
    }

    adminsController.$inject = ['$rootScope', '$location', 'userResource', 'logger', '$state', '$uibModal'];

    angular
        .module('app.admins')
        .component('admins', {
            restrict: 'E',
            scope: {},
            templateUrl: "app/functional/administrators/admins.template.html",
            controller: adminsController,
            controllerAs: "vm" // OR use $ctrl in HTML template
        });
})();