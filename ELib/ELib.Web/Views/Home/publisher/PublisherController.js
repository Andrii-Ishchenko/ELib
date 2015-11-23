(function () {
    angular.module("elib")
           .controller("PublisherController", PublisherController);

    PublisherController.$inject = ["dataServiceFactory", '$routeParams', "$resource", 'PUBLISH_CONST'];

    function PublisherController(dataServiceFactory, $routeParams, $resource, PUBLISH_CONST) {
        var vm = this;

        //gets publisher object
        //GET api/publishers/id/publisher
        vm.instance = dataServiceFactory.getService("publishers").get({ id: $routeParams.id, property: "publisher" });

        //gets list of books which were published by given publisher
        //GET api/publishers/id/books
        vm.book = dataServiceFactory.getService("publishers").query({ id: $routeParams.id, property : "books" });
    }    
})();