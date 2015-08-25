(function () {
    angular.module("elib")
           .controller("BookController", BooksController);

    BooksController.$inject = ["dataServiceFactory", '$routeParams'];

    function BooksController(dataServiceFactory, $routeParams) {
        var vm = this;
        vm.instance = dataServiceFactory.getById('books', $routeParams.id).query();
        console.log(vm.instance);
    }
})();