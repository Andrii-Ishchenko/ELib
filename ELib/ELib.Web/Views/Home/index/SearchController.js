(function () {
    angular.module("elib")
           .controller("SearchController", SearchController);

    SearchController.$inject = ["dataServiceFactory", "$location"];

    function SearchController(dataServiceFactory, $location) {
        var vm = this;
        vm.query = null;
        vm.sendRequest = function () {
            $location.path('/books/search');
            $location.search({ query: vm.query });
        };
    }
})();