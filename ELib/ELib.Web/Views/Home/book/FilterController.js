(function () {
    angular.module("elib")
           .controller("FilterController", FilterController);

    FilterController.$inject = ['dataServiceFactory', "$location", "$route", "$routeParams", "$scope"];

    function FilterController(dataServiceFactory, $location, $route, $routeParams, $scope) {
        vm = this;
        vm.currentYear = new Date().getFullYear();
        vm.genreName = false;
        vm.pageCount = ($routeParams.pageCount) ? $routeParams.pageCount : "5";

        if ($routeParams.categoryId) {
            vm.categoryId = $routeParams.categoryId;
        }

        vm.filterByCategoryId = function filterByCategoryId (id) {
            vm.categoryId = id;
            preparePath();
            $location.search('categoryId', vm.categoryId);
        }

        vm.IsMatching = function(a){
            return a == $routeParams.categoryId;
        }

        function preparePath() {
            if ($location.path() !== "/books/search") {
                $location.path("/books/search");
                $location.search({
                    title: $routeParams.title,
                    author: $routeParams.author,
                    genre: $routeParams.genre,
                    genreId: $routeParams.genreId,
                    subgenre: $routeParams.subgenre,
                    year: $routeParams.year,
                    categoryId:$routeParams.categoryId,
                    query: $routeParams.query,
                    pageCount: $routeParams.pageCount,
                    pageNumb: $routeParams.pageNumb
                });
            }
        }
    }
})();