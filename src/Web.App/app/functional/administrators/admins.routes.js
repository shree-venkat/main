(function () {
    "use strict";

    function getStates() {
        return [{
			    state: "ListAdmins",
			    config: {
			        url: "/website-administration",
			        template: "<admins></admins>"
			    }
			}];
    }

    function appRun(routeRecorder) {
        routeRecorder.save(getStates());
    }

    appRun.$inject = ["routeRecorder"];

    angular
        .module("app.admins")
        .run(appRun);
})();