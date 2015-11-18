(function () {
    angular.module("elib")
           .controller("BooksController", BooksController);

    BooksController.$inject = ["dataServiceFactory", '$routeParams', "$location", 'BOOK_CONST'];

    function BooksController(dataServiceFactory, $routeParams, $location, BOOK_CONST) {
        var vm = this;
        vm.pageCount = ($routeParams.pageCount) ? $routeParams.pageCount : BOOK_CONST.PAGE_COUNT;
        vm.currPage = ($routeParams.pageNumb) ? $routeParams.pageNumb : BOOK_CONST.CURRENT_PAGE;
        vm.maxSize = BOOK_CONST.MAX_SIZE;
        vm.ordering = fetchOrderingWithDefaultParams();
        vm.pageChanged = pageChanged;

        var parameters = getParameters();
        vm.books = dataServiceFactory.getService('books')
                                     .query(parameters, onSuccess = function (response, headers) {
                                         vm.totalCount = headers("totalCount");
                                     },
                                                        onError = function (response) {
                                                            console.log(response.status);
                                                        });

        vm.OrderingChanged = function () {
            var params = getParameters();
            vm.books = dataServiceFactory.getService('books')
                             .query(params, onSuccess = function (response, headers) {
                                 vm.totalCount = headers("totalCount");
                             },
                                                onError = function (response) {
                                                    console.log(response.status);
                                                });
        };

        function fetchOrderingWithDefaultParams() {
            return {
                defaultOrder: "Author",
                defaultDirection: "DESC",
                orderBy: ($routeParams.orderBy) ? $routeParams.orderBy : "Author",
                orderDirection: ($routeParams.orderDirection) ? $routeParams.orderDirection : "DESC",
                orderParameters: ["Title", "Year", "Author", "Publisher", "Rating", "Date"]
            }
        }
    
        function getParameters() {
            return {
                pageCount: vm.pageCount,
                pageNumb: vm.currPage,
                query: $routeParams.query,
                title: $routeParams.title,
                authorName: $routeParams.authorName,
                genre: $routeParams.genre,
                genreId: $routeParams.genreId,
                subgenre: $routeParams.subgenre,
                subgenreId: $routeParams.subgenreId,
                categoryId: $routeParams.categoryId,
                year: $routeParams.year,
                orderBy: vm.ordering.orderBy,
                orderDirection: vm.ordering.orderDirection
            };
        }

        function pageChanged() {
            $location.search(getParameters());
        }

        vm.NodeButtonState = function (node) {

        };
    }
})();