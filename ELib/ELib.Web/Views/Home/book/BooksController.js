﻿(function () {
    angular.module("elib")
           .controller("BooksController", BooksController);

    BooksController.$inject = ["dataServiceFactory", '$routeParams', "$location"];

    function BooksController(dataServiceFactory, $routeParams, $location) {
        var vm = this;
        vm.pageCount = ($routeParams.pageCount) ? $routeParams.pageCount : "5";
        vm.currPage = ($routeParams.pageNumb) ? $routeParams.pageNumb : 1;

        vm.OrderingChanged = function () {

            var params = getParameters();
            var obj = dataServiceFactory.getService('books').get(params)
                .$promise.then(function (data) {
              vm.books = data.books;
              vm.totalCount = data.totalCount;
              vm.totalPages = Math.ceil(vm.totalCount / vm.pageCount);
              vm.maxSize = 5;
              vm.pages = new Array(vm.totalPages);
          })
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

        vm.books = dataServiceFactory.getService('books')
                                     .getWithTotalCount(parameters, onSuccess = function (response) {
                                                                         vm.totalCount = response.totalCount;
                                                                         vm.books = response.items;
                                                                                },
                                                                    onError = function (response) {
                                                                                            console.log(response.message);
                                                                              });
        vm.maxSize = 5;
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