(function () {
    "use strict";

    function errorInterceptor($q, logger) {

        function getError(reason) {
            if (reason.data) {
                return reason.data.Message ? reason.data.Message : reason.data;
            } else {
                return reason;
            }
        }

        function responseError(response) {
            switch (response.status) {
                case 200:
                    logger.log("Ok", getError(response));
                    break;
                case 401:
                    logger.log("Unauthorized", getError(response));
                    break;
                case 404:
                    logger.log("Not Found", getError(response));
                    break;
                case 500:
                    logger.log("Server Error", getError(response));
                    break;
                default:
                    logger.log("Error", getError(response));
                    break;
            }

            return $q.reject(response);
        }

        return {
            responseError: responseError
        };
    }

    errorInterceptor.$inject = ["$q", "logger"];

    function registerInterceptor($httpProvider) {
        $httpProvider.interceptors.push("errorInterceptor");
    }

    registerInterceptor.$inject = ["$httpProvider"];

    angular
       .module("app.common.errorhandler")
       .factory("errorInterceptor", errorInterceptor)
       .config(registerInterceptor);
})();