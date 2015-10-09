(function () {
    angular.module("elib")
           .controller("FilterForAuthorsController", FilterForAuthorsController);

    FilterForAuthorsController.$inject = ['dataServiceFactory', "$location", "$routeParams"];

    function FilterForAuthorsController(dataServiceFactory, $location, $routeParams) {
        vm = this;
        vm.currentYear = new Date().getFullYear();
        vm.pageCount = ($routeParams.pageCount) ? $routeParams.pageCount : "5";
       
        if ($routeParams.author) {
            if (isValid(vm.author)) {
                vm.author = $routeParams.author;
           }
        }

        if ($routeParams.year) {
            vm.year = $routeParams.year;
        }

        vm.filterByAuthor = function () {
            preparePath();
            $location.search('author', vm.author);
        }

        vm.filterByYear = function () {
            preparePath();
            $location.search('year', vm.year);
        }

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
                    author: $routeParams.author,
                    year: $routeParams.year,
                    query: $routeParams.query,
                    pageCount: $routeParams.pageCount,
                    pageNumb: $routeParams.pageNumb
                });
            }
        }
        function isValid(str) {
            return str !== "" && str !== " ";
        }
    }
})();