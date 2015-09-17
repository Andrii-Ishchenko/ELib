(function () {
    angular.module("elib")
           .controller("PublisherController", PublisherController);

    PublisherController.$inject = ["bookRepository", '$routeParams', "$resource"];

    function PublisherController(bookRepository, $routeParams, $resource) {
        var vm = this;
        vm.instance = getService().get({ id: $routeParams.id });
        vm.book = bookRepository.getBooksForPublisher().query({ id: $routeParams.id });

        function getService() {
            var url = "/api/publishers/id/publisher";
            return $resource(url, { id: '@id' }, {
                update: {
                    method: 'PUT',
                    isArray: false
                }
            });
        }
    }    
})();