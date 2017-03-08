(function () {
    'use strict';

    var app = angular.module('app');
    app.directive('ngEnterKey', function () {
        return function (scope, element, attrs) {
            element.bind("keydown keypress", function (event) {
                if (event.which === 13) {
                    scope.$apply(function () {
                        scope.$eval(attrs.ngEnterKey);
                    });

                    event.preventDefault();
                }
            });
        };
    });
})();
