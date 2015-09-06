(function () {
    angular.module("elib")
           .controller("IndexController", IndexController);

    IndexController.$inject = ["authServiceFactory", '$location'];

    function IndexController(authServiceFactory, $location) {
        var vm = this;

        authServiceFactory.fillAuthData();

        vm.logOut = function () {
            authServiceFactory.logOut();
            $location.path('/books');
        }
        vm.authentication = authServiceFactory.authentication;
    }
})();