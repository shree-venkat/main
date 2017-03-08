(function () {
    "use strict";

    function getStates() {
        return [{
            state: "profile",
            config: {
                pageTitle: "Shree Venkat",
                url: "/profile/cv",
                template: "<profile></profile>"
            }
        }];
    }

    function appRun(routeRecorder) {
        routeRecorder.save(getStates());
    }

    appRun.$inject = ["routeRecorder"];

    angular
        .module("app.profile")
        .run(appRun);
})();