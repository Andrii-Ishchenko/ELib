﻿(function () {
    angular.module("elib")
           .controller("AuthorsController", AuthorsController);

    AuthorsController.$inject = ["dataServiceFactory", '$routeParams', "$location"];

    function AuthorsController(dataServiceFactory, $routeParams, $location) {
        var vm = this;
        vm.pageCount = ($routeParams.pageCount) ? $routeParams.pageCount : "5";
        vm.currPage = ($routeParams.pageNumb) ? $routeParams.pageNumb : 1;

        vm.OrderingChanged = function () {
            var params = getParameters();
            var obj = dataServiceFactory.getService('authors').get(params)
                            .$promise.then(function (data) {
                                vm.authors = data.authors;
                                vm.totalCount = data.totalCount;
                                vm.totalPages = Math.ceil(vm.totalCount / vm.pageCount);
                                vm.maxSize = 5;
                                vm.pages = new Array(vm.totalPages);
                            })
        }

        vm.ordering = fetchOrderingWithDefaultParams();

        function fetchOrderingWithDefaultParams() {
            return {
                defaultOrder: "LastName",
                defaultDirection: "DESC",
                orderBy: ($routeParams.orderBy) ? $routeParams.orderBy : "LastName",
                orderDirection: ($routeParams.orderDirection) ? $routeParams.orderDirection : "DESC",
                orderParameters: ["FirstName", "LastName", "DateOfBirth"]
            }
        }
        var parameters = getParameters();


        vm.pageChanged = pageChanged;

        var obj = dataServiceFactory.getService('authors').get(parameters).$promise.then(function (data) {
            vm.authors = data.authors;
            vm.totalItems = data.totalCount;
            vm.totalPages = Math.ceil(vm.totalCount / vm.pageCount);
            vm.maxSize = 5;
        })

        function getParameters() {
            return {
                pageCount: vm.pageCount,
                pageNumb: vm.currPage,
                query: $routeParams.query,
                authorName: $routeParams.authorName,
                year: $routeParams.year,
                orderBy: vm.ordering.orderBy,
                orderDirection: vm.ordering.orderDirection
            };
        }

        function pageChanged() {
            $location.search(getParameters());
        }
    }
})();
