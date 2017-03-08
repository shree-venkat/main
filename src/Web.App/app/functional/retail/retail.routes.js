(function () {
    "use strict";

    function getStates() {
        return [
            {
                state: "retail",
                config: {
                    pageTitle: "Retail Shopping",
                    url: "/retail-product-list",
                    template: "<product-list></product-list>",
                    params: { searchTerm: null, priceFrom: null, priceTo: null, orderProperty: null }
                }
            },
            {
                state: "product",
                config: {
                    pageTitle: "Product Details",
                    url: "/retail-product/:productId",
                    template: "<product-details></product-details>",
                    params: { searchTerm: null, priceFrom: null, priceTo: null, orderProperty: null }
                }
            }
        ];
    }

    function appRun(routeRecorder) {
        routeRecorder.save(getStates());
    }

    appRun.$inject = ["routeRecorder"];

    angular
        .module("app.retail")
        .run(appRun);
})();