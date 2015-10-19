﻿(function () {
    angular.module("elib")
           .controller("AuthorsController", AuthorsController);

    AuthorsController.$inject = ["dataServiceFactory", '$routeParams', "$location"];

    function AuthorsController(dataServiceFactory, $routeParams, $location) {
        var vm = this;
        vm.pageCount = ($routeParams.pageCount) ? $routeParams.pageCount : "5";
        vm.currPage = ($routeParams.pageNumb) ? $routeParams.pageNumb : 1;

        vm.OrderingChanged = function () {
            vm.orderDirection = ($routeParams.orderDirection) ? $routeParams.orderDirection : 'DESC';
            //server post should be here after updating parameters object
            vm.orderParameters = ["FirstName", "LastName", "Date of birth"];
        };

        vm.ordering = {
            orderBy: ($routeParams.orderBy) ? $routeParams.orderBy : 'FirstName',
            orderDirection: ($routeParams.orderDirection) ? $routeParams.orderDirection : 'DESC',
            defaultOrder: "FirstName",
            defaultDirection: "DESC",
            orderParameters: ["FirstName", "LastName", "Date of birth"]
        };

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
                year: $routeParams.year
            };
        }

        function pageChanged() {
            $location.search(getParameters());
        }
    }
})();
