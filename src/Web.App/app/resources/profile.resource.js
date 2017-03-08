(function () {
    'use strict';

    function profileResource($resource) {
        var resource = $resource("app/resources/data/profile/profile.data.json", {}, {
            "get": {
                method: "GET",
                isArray: false
            }
        });

        return resource;
    }

    angular
        .module('app.resources')
        .factory('profileResource', ["$resource", profileResource]);
})();