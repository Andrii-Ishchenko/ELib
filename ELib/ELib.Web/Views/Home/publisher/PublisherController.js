(function () {
    angular.module("elib")
           .controller("PublisherController", PublisherController);

    PublisherController.$inject = ["dataServiceFactory", "bookRepository", '$routeParams'];

    function PublisherController(dataServiceFactory, bookRepository, $routeParams) {
        var vm = this;
        vm.instance = dataServiceFactory.getService("publishers").get({ id: $routeParams.id });
        vm.book = bookRepository.getBooksForPublisher().query({ id: $routeParams.id });
    }
})();