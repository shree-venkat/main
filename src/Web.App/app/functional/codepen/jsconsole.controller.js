(function () {
    'use strict';

    function jsconsoleController($uibModalInstance, $sce) {
        var vm = this;

        var clearJsConsole = function () {
            vm.consoleInput = '';
            vm.consoleOutput = '';
        };

        vm.clearJsConsole = clearJsConsole;

        var initJsConsole = function () {
            vm.clearJsConsole();
            vm.consoleInput = "'test'.replace('s', 'x'); //click play...";
        };

        vm.initJsConsole = initJsConsole;

        vm.initJsConsole();

        var clearJsConsoleInput = function () {
            vm.consoleInput = '';
        };

        vm.clearJsConsoleInput = clearJsConsoleInput;

        var runJsConsole = function () {
            if (vm.consoleInput) {
                var command = '<div> « <span>' + vm.consoleInput + '</span></div>';

                var result = '';
                try {
                    result = window.eval(vm.consoleInput); // OR $.globalEval(vm.consoleInput) OR (window.execScript || window.eval)(vm.consoleInput);
                }
                catch (e) {
                    result = '<span style="color: #d00">' + e + '</span>';
                }

                if (result === undefined) {
                    result = '<span style="color: #d00">undefined</span>';
                }
                else {
                    result = '<span style="color: #090">' + result + '</span>';
                }

                var response = '<div> » ' + result + '</div>';
                vm.consoleOutput = $sce.trustAsHtml(vm.consoleOutput + command + response + '<div class="separator"></div>');

                $("#js-console-output-parent").animate({ scrollTop: $('#js-console-output').prop("scrollHeight") }, 500);

                vm.consoleInput = '';
            }
        };

        vm.runJsConsole = runJsConsole;

        vm.close = function () {
            $uibModalInstance.dismiss("cancel");
        };
    }

    jsconsoleController.$inject = ['$uibModalInstance', "$sce"];

    angular
        .module('app.codepen')
        .controller('jsconsoleController', jsconsoleController);
})();