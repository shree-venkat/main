(function () {
    "use strict";

    var app = angular.module("app");

    app.config(["$locationProvider", "localStorageServiceProvider", "cfpLoadingBarProvider",
        function ($locationProvider, localStorageServiceProvider, cfpLoadingBarProvider) {

            $locationProvider.hashPrefix("");

            localStorageServiceProvider.setPrefix("TestYourTemp");

            cfpLoadingBarProvider.includeSpinner = false;
            cfpLoadingBarProvider.latencyThreshold = 500;

            /*$provide.decorator("$exceptionHandler", ["$window", "errorResource", function ($window, errorResource) {
                return function (exception, cause) {
                    var errorDetails = {
                        Message: exception.toString(),
                        Url: $window.location.href,
                        Cause: cause,
                        FileName: (exception.fileName) ? exception.filename : '',
                        LineNumber: (exception.lineNumber) ? exception.lineNumber : '',
                        StackTrace: (exception.stack) ? exception.stack : ''
                    };

                    errorResource.log({ errorDetails: errorDetails });
                };
            }]);*/
        }
    ]);
})();