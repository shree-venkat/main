(function () {
    "use strict";

    function logger($log, toaster) {

        function log(title, message) {
            $log.log(title + ": " + message);
        }

        function success(title, message) {
            toaster.success(message, title);
            $log.info("Success: " + message);
        }

        function info(title, message) {
            toaster.info(message, title);
            $log.info("Info: " + message);
        }

        function warning(title, message) {
            toaster.warning(message, title);
            $log.warn("Warning: " + message);
        }

        function error(title, message, data) {
            toaster.error(message, title);
            $log.error("Error: " + message, data);
        }

        return {
            log: log,
            success: success,
            info: info,
            warning: warning,
            error: error
        };
    }

    logger.$inject = ["$log", "toaster"];

    angular
        .module("app.common.logger")
        .factory("logger", logger);
}());