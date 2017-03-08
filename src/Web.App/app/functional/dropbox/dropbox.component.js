(function () {
    'use strict';

    function dropboxController($rootScope, $location, dropboxResource, apiBaseUrl, logger, $state, $uibModal) {
        $rootScope.bannerTitle = "Dropbox/GDrive";
        $rootScope.bannerSubtitle = "Dropbox or Google Drive - A simple look at file sharing/storage solution using AngularJS 1.x";

        var vm = this;

        vm.page = ($location.search().page == null ? 1 : $location.search().page);
        vm.pageSize = ($location.search().pageSize == null ? "10" : $location.search().pageSize);
        vm.searchTerm = ($location.search().searchTerm == null ? '' : $location.search().searchTerm);
        vm.totalPages = 0;
        vm.total = 0;

        vm.files = [];

        var loadFiles = function () {
            $rootScope.showLoadingMessage = "Loading files ...";
            dropboxResource.get({ page: vm.page, pageSize: vm.pageSize, searchTerm: vm.searchTerm }).$promise
                .then(function (data) {
                    vm.files = data.Items;
                    vm.totalPages = data.TotalPages;
                    vm.total = data.TotalCount;
                }).catch(function (error) {
                    logger.error("Error", "Unable to retrieve files", error);
                }).finally(function () {
                    $rootScope.showLoadingMessage = false;
                });
        };

        vm.loadFiles = loadFiles;

        vm.isAdmin = function (user) {
            if (user.Roles.indexOf('Administrator') >= 0) {
                return true;
            } else {
                return false;
            }
        };

        vm.search = function (searchTerm) {
            vm.page = 1;
            vm.searchTerm = searchTerm;
            vm.loadFiles();
        };

        vm.pageSizeChanged = function () {
            vm.page = 1;
            vm.loadFiles();
        };

        vm.pageChanged = function () {
            vm.loadFiles();
        };

        var uploadFile = function () {
            window.location.href = "http://CMS.ShreeVenkat.co.uk/GdriveAdmin/Index";
        };

        var getDownloadFileLink = function (file) {
            return apiBaseUrl + "dropbox?id=" + file.Id;
        };

        var deleteFile = function (file) {
            var result = false;
            var message = 'Are you sure you want to remove "' + file.FileName + '" file?';
            vm.modalInstance = $uibModal.open({
                templateUrl: 'app/shared/confirmModal.html?v=' + Math.random().toString(36).slice(2),
                controller: 'confirmController',
                controllerAs: 'vm',
                windowClass: 'modal-container',
                resolve: {
                    confirmTitle: function () { return "Confirm"; },
                    confirmMessage: function () { return message; },
                    confirmOkName: function () { return "Delete"; },
                    confirmCancelName: function () { return "Cancel"; }
                }
            });

            vm.modalInstance.result.then(function (response) {
                result = response;

                if (result) {
                    window.location.href = "http://CMS.ShreeVenkat.co.uk/GdriveAdmin/Index";
                }
            }).catch(function () {
            });
        };

        vm.uploadFile = uploadFile;
        vm.getDownloadFileLink = getDownloadFileLink;
        vm.deleteFile = deleteFile;

        vm.loadFiles();
    }

    dropboxController.$inject = ['$rootScope', '$location', 'dropboxResource', 'apiBaseUrl', 'logger', '$state', '$uibModal'];

    angular
        .module('app.dropbox')
        .component('dropbox', {
            restrict: 'E',
            scope: {},
            templateUrl: "app/functional/dropbox/dropbox.template.html",
            controller: dropboxController,
            controllerAs: "vm" // OR use $ctrl in HTML template
        });
})();