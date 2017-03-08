(function () {
    'use strict';

    function errorResource($resource, apiBaseUrl) {
        var resource = $resource(apiBaseUrl + "error", {}, {
            "get": { method: "GET" },
            "log": { method: "POST" }
        });

        return resource;
    }

    angular
        .module('app.resources')
        .factory('errorResource', ["$resource", "apiBaseUrl", errorResource]);
})();