(function () {
    angular.module("elib")
           .controller("AuthorController", AuthorController);

    AuthorController.$inject = ["dataServiceFactory", '$routeParams'];

    function AuthorController(dataServiceFactory, $routeParams) {
        var vm = this;
        vm.instance = dataServiceFactory.getService('authors').get({ id: $routeParams.id });
      
    }
})();