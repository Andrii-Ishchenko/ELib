(function () {
    angular.module("elib")
           .controller("FilterController", FilterController);

    FilterController.$inject = ['dataServiceFactory', "$location", "$routeParams"];

    function FilterController(dataServiceFactory, $location, $routeParams) {
        vm = this;
        vm.currentYear = new Date().getFullYear();
        vm.genreName = false;
        vm.pageCount = ($routeParams.pageCount) ? $routeParams.pageCount : "5";

        var obj = dataServiceFactory.getService('genres').query().$promise.then(function (data) {
            vm.genres = data;
            if ($routeParams.genreId > 0) {
                vm.genreName = vm.genres[$routeParams.genreId - 1].Name;
            }
        });

        vm.addFilter = function (value, property, isString) {
            var valid = true;
            if (isString) {
                valid = isValid(value);
            }
            if (valid) {
                preparePath();
                $location.search(property, value);
            }
        }

        vm.removeFilter = function(property){
            vm[property] = undefined;
            $location.search(property, undefined);
        }

if ($routeParams.categoryId) {
            vm.categoryId = $routeParams.categoryId;
            vm.i = vm.categoryId;
        }
        vm.filterByCategoryId = function filterByCategoryId (id) {
            vm.categoryId = id;
            preparePath();
            $location.search('categoryId', vm.categoryId);
        }
        vm.changePageCount = function () {
            if ($location.url() === "/books") {
                $location.search({ pageCount: vm.pageCount });
            }
            else {
                $location.search("pageCount", vm.pageCount);
            }
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
                    categoryId:undefined,
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