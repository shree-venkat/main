(function () {
    "use strict";

    function getStates() {
        return [{
            state: "dropbox",
            config: {
                pageTitle: "Storage",
                url: "/dropbox-google-drive-simulation",
                template: "<dropbox></dropbox>"
            }
        }];
    }

    function appRun(routeRecorder) {
        routeRecorder.save(getStates());
    }

    appRun.$inject = ["routeRecorder"];

    angular
        .module("app.dropbox")
        .run(appRun);
})();