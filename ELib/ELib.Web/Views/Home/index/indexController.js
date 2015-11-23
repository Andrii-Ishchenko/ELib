(function () {
    angular.module("elib")
           .controller("IndexController", IndexController);

    IndexController.$inject = ["authServiceFactory", "dataServiceFactory", '$location', 'INDEX_CONST'];

    function IndexController(authServiceFactory, dataServiceFactory, $location, INDEX_CONST) {
        var vm = this;

        authServiceFactory.fillAuthData();

        vm.logOut = function () {
            authServiceFactory.logOut();
            $location.path(INDEX_CONST.BOOKS);
        }
        vm.authentication = authServiceFactory.authentication;

        vm.genres = dataServiceFactory.getService('genres').query();
    }
})();