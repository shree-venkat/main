(function () {
    "use strict";

    angular.module("app", [
            'ngRoute',
            'ngResource',
            'ngAnimate',
            'ngSanitize',
            'ngTouch',
            'ngMessages',
            'ui.bootstrap',
            'ui.router',
            'ui.router.state.events',
            "ui.router.tabs",
            'ui.select',
            'ui-rangeSlider',
            'LocalStorageModule',

            'angular-loading-bar',
            'toaster',
            'angular.filter',

            'app.common.errorhandler',
            'app.common.logger',
            'app.common.router',

            "app.resources",
            "app.navbar",
            "app.admins",
            "app.retail",
            "app.dropbox",
            "app.codepen",
            "app.home",
            "app.profile",

            'app.settings'
    ]);
})();
