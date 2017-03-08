(function () {
    'use strict';

    function userResource($resource, apiBaseUrl) {
        var resource = $resource(apiBaseUrl + "User", null, {
            "current": { method: "GET", url: apiBaseUrl + "User/GetCurrentUser" },
            "query": { method: "GET", url: apiBaseUrl + "User/GetUsers" },
            "create": { method: "POST", url: apiBaseUrl + "User/Add" },
            "delete": { method: "DELETE", url: apiBaseUrl + "User/Delete" },
            "activeDirectoryAccounts": { method: "GET", url: apiBaseUrl + "User/GetADAccounts", isArray: true }
        });

        return resource;
    }

    angular
        .module('app.resources')
        .factory('userResource', ["$resource", "apiBaseUrl", userResource]);
})();