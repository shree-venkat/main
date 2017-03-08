(function () {
    'use strict';

    angular
        .module('app')
        .filter('formattedSize', function () {
            return function (bytes) {
                if (bytes === 0) {
                    return '0 Bytes';
                }

                var factor = 1024,
                    decimals = 2,
                    sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'],
                    category = Math.floor(Math.log(bytes) / Math.log(factor));

                return parseFloat((bytes / Math.pow(factor, category)).toFixed(decimals)) + ' ' + sizes[category];
            };
        });
}());
