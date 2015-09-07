(function () {
    angular.module("elib")
           .controller("PublisherController", PublisherController);

    PublisherController.$inject = ["dataServiceFactory", '$routeParams'];

    function PublisherController(dataServiceFactory, $routeParams) {
        var vm = this;
        vm.instance = dataServiceFactory.getService("publishers").get({ id: $routeParams.id });  
    }
})();