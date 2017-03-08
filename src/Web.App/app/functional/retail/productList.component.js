(function () {
    'use strict';

    function productList($rootScope, $state, $stateParams, $sce, retailResource, logger) {
        $rootScope.bannerTitle = "Retail Shopping";
        $rootScope.bannerSubtitle = "Managing your retail portfolio - Showcasing how it's made using AngularJS";
        $rootScope.customBannerHtml = $sce.trustAsHtml("<h6>* The CONTENT in this demo ('.json' data & images) are from <a href='https://github.com/angular/angular-phonecat' target='_blank'>PhoneCat Tutorial App</a>, so big thanks to them for saving me a marathon effort</h6>");

        var vm = this;
        var defaultPriceFrom = 180;
        var defaultPriceTo = 800;
        var defaultOrderProperty = 'age';

        vm.products = [];
        vm.searchTerm = '';
        vm.priceFrom = defaultPriceFrom;
        vm.priceTo = defaultPriceTo;
        vm.orderProperty = defaultOrderProperty;

        if ($stateParams.searchTerm) {
            vm.searchTerm = $stateParams.searchTerm;
        }
        if ($stateParams.priceFrom) {
            vm.priceFrom = $stateParams.priceFrom;
        }
        if ($stateParams.priceTo) {
            vm.priceTo = $stateParams.priceTo;
        }
        if ($stateParams.orderProperty) {
            vm.orderProperty = $stateParams.orderProperty;
        }

        var applyFilters = function (item) {
            if ((!vm.searchTerm || (item.name.toLowerCase().indexOf(vm.searchTerm.toLowerCase()) > -1)) &&
                (!vm.priceFrom || (item.nowPrice >= vm.priceFrom)) && (!vm.priceTo || (item.nowPrice <= vm.priceTo))) {
                return true;
            }

            return false;
        };

        var resetFilters = function () {
            vm.searchTerm = '';
            vm.priceFrom = defaultPriceFrom;
            vm.priceTo = defaultPriceTo;
            vm.orderProperty = defaultOrderProperty;
        };

        var showResetFilters = function () {
            if (vm.searchTerm || vm.priceFrom !== defaultPriceFrom || vm.priceTo !== defaultPriceTo) {
                return true;
            }

            return false;
        };

        var loadProducts = function () {
            $rootScope.showLoadingMessage = "Loading products ...";
            retailResource.query().$promise.then(function (data) {
                vm.products = data;
            }).catch(function (error) {
                logger.error("Error", "Sorry, an error occurred while retrieving data", error);
            }).finally(function () {
                $rootScope.showLoadingMessage = false;
            });
        };

        var getDetails = function (product) {
            $state.go("product", { productId: product.id, searchTerm: vm.searchTerm, priceFrom: vm.priceFrom, priceTo: vm.priceTo, orderProperty: vm.orderProperty });
        };

        vm.applyFilters = applyFilters;
        vm.resetFilters = resetFilters;
        vm.showResetFilters = showResetFilters;
        vm.loadProducts = loadProducts;
        vm.getDetails = getDetails;

        vm.loadProducts();
    }

    productList.$inject = ['$rootScope', '$state', '$stateParams', '$sce', 'retailResource', 'logger'];

    angular
        .module('app.retail')
        .component('productList', {
            restrict: 'E',
            scope: {},
            templateUrl: "app/functional/retail/productList.template.html",
            controller: productList,
            controllerAs: "vm" // OR use $ctrl in HTML template
        });
})();