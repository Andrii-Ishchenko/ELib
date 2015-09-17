(function () {
    angular.module("elib")
           .controller("HelpController", HelpController);

    HelpController.$inject = ['$window'];

    function HelpController($window) {
        var vm = this;
        $window.location.reload();
    }
})();