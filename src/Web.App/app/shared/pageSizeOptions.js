(function () {
    'use strict';
    var app = angular.module('app');

    app.directive('pageSizeOptions', function () {
        return {
            restrict: 'E',
            templateUrl: 'app/shared/pageSizeOptions.html'
        };
    });
})();