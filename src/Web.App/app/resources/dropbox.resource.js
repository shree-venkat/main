(function () {
    'use strict';

    function dropboxResource($resource, apiBaseUrl) {
        var resource = $resource(apiBaseUrl + "dropbox", {}, {
            "get": { method: "GET" },
            "getById": { method: "GET" }
        });

        return resource;
    }

    angular
        .module('app.resources')
        .factory('dropboxResource', ["$resource", "apiBaseUrl", dropboxResource]);
})();