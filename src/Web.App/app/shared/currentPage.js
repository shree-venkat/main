(function () {
    'use strict';
    var app = angular.module('app');

    app.directive('currentPage', function () {
        return {
            restrict: 'E',
            templateUrl: 'app/shared/currentPage.html'
        };
    });
})();