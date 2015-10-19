(function () {
    angular.module("elib")
           .controller("FilterForAuthorsController", FilterForAuthorsController);

    FilterForAuthorsController.$inject = ['dataServiceFactory', "$location", "$routeParams"];

    function FilterForAuthorsController(dataServiceFactory, $location, $routeParams) {
        vm = this;
        vm.pageCount = ($routeParams.pageCount) ? $routeParams.pageCount : "5";
       
            vm.changePageCount = function () {
            if ($location.path() === "/authors") {
                $location.search({ pageCount: vm.pageCount });
            }
            else {
                $location.search("pageCount", vm.pageCount);
            }
        }

        function preparePath() {
            if ($location.path() !== "/authors/search") {
                $location.path("/authors/search");
                $location.search({
                    authorName: $routeParams.authorName,
                    year: $routeParams.year,
                    query: $routeParams.query,
                    pageCount: $routeParams.pageCount,
                    pageNumb: $routeParams.pageNumb
                });
            }
        }
    }
})();