(function () {
    'use strict';

    function createAdminController($location, userResource, $filter, logger, $uibModalInstance) {

        var vm = this;

        vm.submitButtonName = "Create";
        vm.pageTitle = "Create";
        vm.user = {};

        vm.getActiveDirectoryUsers = function (searchTerm) {
            return userResource.activeDirectoryAccounts({ searchTerm: searchTerm }).$promise
                    .then(function (data) {
                        return $filter('limitTo')(data, 7);
                    });
        };

        vm.userNameOnSelect = function (item, formInput) {
            formInput.$setValidity('parse', true);
            vm.setUserNameValues(item);
        };

        vm.setUserNameValues = function (item) {
            vm.user.Id = null;
            vm.user.Forename = item.Forename;
            vm.user.Surname = item.Surname;
            vm.user.DisplayName = item.DisplayName;
            vm.user.UserName = item.Username;
            vm.user.EmailAddress = item.EmailAddress;
            vm.user.IsActive = true;
        };

        vm.submit = function () {
            vm.error = null;

            return userResource.create(vm.user).$promise.then(function () {
                    $uibModalInstance.close();
                    logger.success("Success", vm.user.DisplayName + " is added successfully", "");
                }).catch(function (response) {
                    logger.error('Error', 'Unable to add user', response.data);
                });
        };

        vm.close = function () {
            $uibModalInstance.dismiss("cancel");
        };
    }

    createAdminController.$inject = ['$location', 'userResource', '$filter', 'logger', '$uibModalInstance'];

    angular
        .module('app.admins')
        .controller('createAdminController', createAdminController);
})();