(function () {
    angular.module("elib")
           .controller("SearchController", SearchController);

    SearchController.$inject = ["$location", "$routeParams", 'INDEX_CONST'];

    function SearchController($location, $routeParams, INDEX_CONST) {
        var vm = this;
        vm.query =$routeParams.query;
        vm.type = INDEX_CONST.TYPE_BOOKS;
        vm.sendRequest = function () {
            if (vm.type === INDEX_CONST.TYPE_BOOKS) {
                $location.path(INDEX_CONST.BOOK_SEARCH);
                $location.search({ query: vm.query });
            }
            if (vm.type === INDEX_CONST.TYPE_AUTHORS) {
                $location.path(INDEX_CONST.AUTHOR_SEARCH);
                $location.search({ query: vm.query });
            }
            if (vm.type === INDEX_CONST.TYPE_PUBLISHERS) {
                $location.path(INDEX_CONST.PUBLISHER_SEARCH);
                $location.search({ query: vm.query });
            }
        };
    }
})();
