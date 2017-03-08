(function () {
    'use strict';

    function homeResource($resource, apiBaseUrl) {
        var resource = $resource(apiBaseUrl + "Email/Contact", {}, {
            "sendEmail": { method: "GET" }
        });

        return resource;
    }

    angular
        .module('app.resources')
        .factory('homeResource', ["$resource", "apiBaseUrl", homeResource]);
})();