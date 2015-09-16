(function () {
    angular.module("elib")
           .controller("AuthorController", AuthorController);

    AuthorController.$inject = ["bookRepository", '$routeParams', "$resource"];

    function AuthorController(bookRepository, $routeParams, $resource) {
        var vm = this;
        vm.instance = getService().get({ id: $routeParams.id });
        vm.books = bookRepository.getBooksForAuthor().query({ id: $routeParams.id });

        function getService() {
            var url = "/api/authors/id/author";
            return $resource(url, { id: '@id' }, {
                update: {
                    method: 'PUT',
                    isArray: false
                }
            });
        }
    }
})();