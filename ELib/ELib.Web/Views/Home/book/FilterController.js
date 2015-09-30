(function () {
    angular.module("elib")
           .controller("FilterController", FilterController);

    FilterController.$inject = ['dataServiceFactory', "$location", "$routeParams"];

    function FilterController(dataServiceFactory, $location, $routeParams) {
        vm = this;
        vm.currentYear = new Date().getFullYear();
        vm.genreName = false;
        vm.pageCount = ($routeParams.pageCount) ? $routeParams.pageCount : 5;

        var obj = dataServiceFactory.getService('genres').query();
        obj.$promise.then(function (data) {
            vm.genres = data;
            if ($routeParams.genreId > 0) {
                vm.genreName = vm.genres[$routeParams.genreId - 1].Name;
            }
        });

        if ($routeParams.title) {
            vm.title = $routeParams.title;
        }

        if ($routeParams.author) {
            vm.author = $routeParams.author;
        }

        if ($routeParams.genre) {
            vm.genre = $routeParams.genre;
        }

        if ($routeParams.genreId) {
            vm.genreId = $routeParams.genreId;
        }

        if ($routeParams.subgenre) {
            vm.subgenre = $routeParams.subgenre;
        }

        if ($routeParams.year) {
            vm.year = $routeParams.year;
        }

        vm.filterByTitle = function () {
            preparePath();
            $location.search('title', vm.title);
        }

        vm.filterByAuthor = function () {
            preparePath();
            $location.search('author', vm.author);
        }

        vm.filterByGenre = function () {
            preparePath();
            $location.search('genre', vm.genre);
        }

        vm.filterByGenreId = function () {
            preparePath();
            $location.search('genreId', vm.genreId);
        }

        vm.filterBySubgenre = function () {
            preparePath();
            $location.search('subgenre', vm.subgenre);
        }

        vm.filterByYear = function () {
            preparePath();
            $location.search('year', vm.year);
        }

        vm.changePageCount = function () {
            if ($location.path() === "/books") {
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
                    title: undefined,
                    author: undefined,
                    genre: undefined,
                    genreId: undefined,
                    subgenre: undefined,
                    year: undefined,
                    query: $routeParams.query,
                    pageCount: $routeParams.pageCount,
                    pageNumb: $routeParams.pageNumb
                });
            }
        }
    }
})();