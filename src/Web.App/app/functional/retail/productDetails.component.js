(function () {
    'use strict';

    function productDetails($rootScope, $state, $stateParams, $sce, retailResource, logger) {
        $rootScope.bannerTitle = "Product details";
        $rootScope.bannerSubtitle = "Managing your retail portfolio - Showcasing how it's made using AngularJS";
        $rootScope.customBannerHtml = $sce.trustAsHtml("<h6>* The CONTENT in this demo ('.json' data & images) are from <a href='https://github.com/angular/angular-phonecat' target='_blank'>PhoneCat Tutorial App</a>, so big thanks to them for saving me a marathon effort</h6>");

        var vm = this;
        vm.product = {};
        vm.productId = $stateParams.productId;

        var loadProduct = function () {
            $rootScope.showLoadingMessage = "Loading your selection ...";
            retailResource.get({ file: vm.productId }).$promise.then(function (data) {
                vm.product = data;
                vm.mainImage = vm.product.images[0];
            }).catch(function (error) {
                logger.error("Error", "Sorry, an error occurred while retrieving data", error);
            }).finally(function () {
                $rootScope.showLoadingMessage = false;
            });
        };

        var backToSearch = function () {
            $state.go("retail", { searchTerm: $stateParams.searchTerm, priceFrom: $stateParams.priceFrom, priceTo: $stateParams.priceTo, orderProperty: $stateParams.orderProperty });
        };

        vm.loadProduct = loadProduct;
        vm.backToSearch = backToSearch;

        vm.loadProduct();
    }

    productDetails.$inject = ['$rootScope', '$state', '$stateParams', '$sce', 'retailResource', 'logger'];

    angular
        .module('app.retail')
        .component('productDetails', {
            restrict: 'E',
            scope: {},
            templateUrl: "app/functional/retail/productDetails.template.html",
            controller: productDetails,
            controllerAs: "vm" // OR use $ctrl in HTML template
        });
})();