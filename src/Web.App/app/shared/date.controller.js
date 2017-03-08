(function () {
    'use strict';
    var app = angular.module('app');

    app.controller('dateController', ['$scope', function ($scope) {
        $scope.open = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            $scope.opened = !$scope.opened;
        };

        $scope.dateOptions = {
            startingDay: 1
        };

        $scope.monthDateOptions = {
            minMode: 'month',
            maxMode: 'month'
        };
        $scope.monthPopupMode = 'month';

        $scope.format = 'dd/MM/yyyy';
        $scope.altInputFormats = ['dd/MM/yyyy', 'dd/M/yyyy', 'd/MM/yyyy', 'd/M/yy', 'd/M/yyyy'];
        $scope.monthFormat = 'MM-yy';
    }]);
})();