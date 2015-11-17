(function () {
    angular.module("elib")
           .controller("BooksController", BooksController);

    BooksController.$inject = ["dataServiceFactory", '$routeParams', "$location", 'BOOK_CONST'];

    function BooksController(dataServiceFactory, $routeParams, $location, BOOK_CONST) {
        var vm = this;
        vm.pageCount = ($routeParams.pageCount) ? $routeParams.pageCount : BOOK_CONST.PAGE_COUNT;
        vm.currPage = ($routeParams.pageNumb) ? $routeParams.pageNumb : BOOK_CONST.CURRENT_PAGE;

        vm.OrderingChanged = function () {

            var params = getParameters();
            dataServiceFactory.getService('books')
                             .getWithTotalCount(parameters, onSuccess = function (response) {
                                                              vm.totalCount = response.totalCount;
                                                              vm.books = response.books;
                                                            },
                                                            onError = function (response) {
                                                                console.log(response.message);
                                                            });
        }


        vm.ordering = fetchOrderingWithDefaultParams();
       
        function fetchOrderingWithDefaultParams() {
            return {
                defaultOrder: "Author",
                defaultDirection: "DESC",
                orderBy: ($routeParams.orderBy) ? $routeParams.orderBy : "Author",
                orderDirection: ($routeParams.orderDirection) ? $routeParams.orderDirection : "DESC",
                orderParameters: ["Title", "Year", "Author", "Publisher", "Rating", "Date"]
            }
        }

        var parameters = getParameters()

        vm.pageChanged = pageChanged;

        dataServiceFactory.getService('books')
                                     .getWithTotalCount(parameters, onSuccess = function (response) {
                                                                         vm.totalCount = response.totalCount;
                                                                         vm.books = response.books;
                                                                                },
                                                                    onError = function (response) {
                                                                                            console.log(response.message);
                                                                              });
        vm.maxSize = BOOK_CONST.MAX_SIZE;
        //.get(parameters)
        //    .$promise.then(function (data) {
        //    vm.books = data.books;
        //    vm.totalCount = data.totalCount;
        //    vm.totalPages = Math.ceil(vm.totalCount / vm.pageCount);
        //    vm.maxSize = 5;
        //    vm.pages = new Array(vm.totalPages);
        //})
      
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
                categoryId : $routeParams.categoryId,
                year: $routeParams.year,
                orderBy: vm.ordering.orderBy,
                orderDirection: vm.ordering.orderDirection
            };
        }
        function pageChanged() {
            $location.search(getParameters());
        }

        vm.NodeButtonState = function (node) {

        }      
    }
})();