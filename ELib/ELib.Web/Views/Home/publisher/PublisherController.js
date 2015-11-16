(function () {
    angular.module("elib")
           .controller("PublisherController", PublisherController);

    PublisherController.$inject = ["bookRepository", '$routeParams', "$resource", 'PUBLISH_CONST'];

    function PublisherController(bookRepository, $routeParams, $resource, PUBLISH_CONST) {
        var vm = this;
        vm.instance = getService().get({ id: $routeParams.id });
        vm.book = bookRepository.getBooksForPublisher().query({ id: $routeParams.id });

        function getService() {
            var url = PUBLISH_CONST.PUBLISHER;
            return $resource(url, { id: '@id' }, {
                update: {
                    method: 'PUT',
                    isArray: false
                }
            });
        }
    }    
})();