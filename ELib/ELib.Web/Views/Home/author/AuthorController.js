(function () {
    angular.module("elib")
           .controller("AuthorController", AuthorController);

    AuthorController.$inject = ["dataServiceFactory", "bookRepository", '$routeParams'];

    function AuthorController(dataServiceFactory, bookRepository, $routeParams) {
        var vm = this;
        vm.instance = dataServiceFactory.getService('authors').get({ id: $routeParams.id });
        vm.books = bookRepository.getBooksForAuthor().query({ id: $routeParams.id });
    }
})();