(function () {
    angular.module("elib")
           .controller("SearchController", SearchController);

    SearchController.$inject = ["$location"];

    function SearchController( $location) {
        var vm = this;
        vm.query = null;
        vm.type = "books";
        vm.sendRequest = function () {
            if (vm.type === "books") {
                $location.path('/books/search');
                $location.search({ query: vm.query });
            }
            if (vm.type === "authors") {
                $location.path('/authors/search');
                $location.search({ query: vm.query });
            }
            if (vm.type === "publishers") {
                $location.path('/publishers/search');
                $location.search({ query: vm.query });
            }
        };
    }
})();