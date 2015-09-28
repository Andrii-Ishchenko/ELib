(function () {
    angular.module("elib")
           .controller("IndexController", IndexController);

    IndexController.$inject = ["authServiceFactory", "dataServiceFactory", '$location'];

    function IndexController(authServiceFactory, dataServiceFactory, $location) {
        var vm = this;

        authServiceFactory.fillAuthData();

        vm.logOut = function () {
            authServiceFactory.logOut();
            $location.path('/books');
        }
        vm.authentication = authServiceFactory.authentication;

        vm.genres = [];
       // vm.genres = dataServiceFactory.getService('genres');
        var obj = dataServiceFactory.getService('genres').query();
        obj.$promise.then(function (data) {
            vm.genres = data;
        })
    }
})();