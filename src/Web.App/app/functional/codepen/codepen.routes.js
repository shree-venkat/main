(function () {
    "use strict";

    function getStates() {
        return [{
            state: "codepen",
            config: {
                pageTitle: "Test Your Code",
                url: "/code-pen-js-fiddle-simulation",
                template: "<codepen></codepen>"
            }
        }];
    }

    function appRun(routeRecorder) {
        routeRecorder.save(getStates());
    }

    appRun.$inject = ["routeRecorder"];

    angular
        .module("app.codepen")
        .run(appRun);
})();