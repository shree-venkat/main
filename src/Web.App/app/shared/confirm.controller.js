(function () {
    'use strict';

    function confirmController($uibModalInstance, confirmTitle, confirmMessage, confirmOkName, confirmCancelName) {

        var vm = this;

        vm.title = "Confirm";
        if (confirmTitle) {
            vm.title = confirmTitle;
        }

        vm.message = "Are you sure you want to continue?";
        if (confirmMessage) {
            vm.message = confirmMessage;
        }

        vm.okName = "Yes";
        if (confirmOkName) {
            vm.okName = confirmOkName;
        }

        vm.cancelName = "No";
        if (confirmCancelName) {
            vm.cancelName = confirmCancelName;
        }

        vm.okAction = function () {
            $uibModalInstance.close(true);
        };

        vm.cancelAction = function () {
            $uibModalInstance.close(false);
        };
    }

    confirmController.$inject = ['$uibModalInstance', 'confirmTitle', 'confirmMessage', 'confirmOkName', 'confirmCancelName'];

    angular
        .module('app')
        .controller('confirmController', confirmController);
})();