(function () {
    'use strict';

    function codepenController($rootScope, $uibModal, $sce) {
        $rootScope.bannerTitle = "CodePen/JSFiddle";
        $rootScope.bannerSubtitle = "CodePen or JSFiddle - A demonstration of how it's made using AngularJS 1.x";

        var vm = this;

        var init = function () {
            vm.html = '<div class="sample">\r    <div>Just what you expected?</div>\r</div>\r';
            vm.less = '@blueShade: #2e6da4;\r.sample {\r    color: @blueShade;\r    font-weight: bold;\r\r    div {\r        padding: 10px;\r    }\r}\r';
            var jsInitCode = '$(".sample").append(\'<button id="clear-demo" class="btn btn-success">DIY Now!</button>\');';
            jsInitCode += '\r\r$("#clear-demo").click(function() {\r    $(".reset").trigger(\'click\'); \r});';
            vm.js = jsInitCode;

            vm.outputStyle = '';
            vm.outputScript = '';
            vm.outputHtml = '';
        };

        var clear = function () {
            vm.html = '';
            vm.less = '';
            vm.js = '';

            vm.outputStyle = '';
            vm.outputScript = '';
            vm.outputHtml = '';
        };

        var clearOutput = function () {
            vm.outputStyle = '';
            vm.outputScript = '';
            vm.outputHtml = '';
        };

        var run = function () {
            vm.outputStyle = '';
            vm.outputScript = '';
            vm.outputHtml = '';

            try {
                var styleContext = '.output-box { ' + vm.less + ' }'; // encapsulate style changes to Pen code

                /* global less */
                less.render(styleContext, function (e, output) {
                    vm.outputStyle = output.css;
                });

                if (vm.html) {
                    vm.outputScript = $sce.trustAsHtml('<script type="text/javascript">' + vm.js + '</script>');
                    vm.outputHtml = '<div>' + vm.html + '</div>';
                }
            } catch (e) {
                window.console.log(e);
                vm.outputHtml = '<div style="background:#fff0f0; color: red">' + vm.html + '</div>';
            }
        };

        var openJsConsole = function () {
            vm.modalInstance = $uibModal.open({
                templateUrl: 'app/functional/codepen/jsconsole.modal.html?bust=' + Math.random().toString(36).slice(2),
                controller: 'jsconsoleController',
                controllerAs: 'vm',
                size: 'md',
                windowClass: 'modal-container'
            });

            vm.modalInstance.result.then(function () {
                vm.clearJsConsole();
            }).catch(function () {
            });
        };

        vm.init = init;
        vm.clear = clear;
        vm.clearOutput = clearOutput;
        vm.run = run;
        vm.openJsConsole = openJsConsole;

        vm.init();
    }

    codepenController.$inject = ['$rootScope', '$uibModal', "$sce"];

    angular
        .module('app.codepen')
        .component('codepen', {
            restrict: 'E',
            scope: {},
            templateUrl: "app/functional/codepen/codepen.template.html",
            controller: codepenController,
            controllerAs: "vm" // OR use $ctrl in HTML template
        });
})();