﻿(function () {
    'use strict';

    angular
        .module('app')
        .filter('checkmark', function () {
            return function (input) {
                return input ? '\u2713' : '\u2718';
            };
        });
}());