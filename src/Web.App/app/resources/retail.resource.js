(function () {
    'use strict';

    function retailResource($resource) {
        var resource = $resource("app/resources/data/retail/:file.json", {}, {
            "query": {
                method: "GET",
                params: {file: "phones"},
                isArray: true
            }
        });

        return resource;
    }

    angular
        .module('app.resources')
        .factory('retailResource', ["$resource", retailResource]);
})();