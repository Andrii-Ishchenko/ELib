(function () {
    angular.module("elib")
           .controller("AuthorsController", AuthorsController);

    AuthorsController.$inject = ["dataServiceFactory", '$routeParams', "$location"];

    function AuthorsController(dataServiceFactory, $routeParams, $location) {
        var vm = this;
        vm.pageCount = ($routeParams.pageCount) ? $routeParams.pageCount : 5;
        vm.currPage = ($routeParams.pageNumb) ? $routeParams.pageNumb : 1;

        var obj = dataServiceFactory.getService('authors').get({ pageCount: vm.pageCount, pageNumb: vm.currPage, query: $routeParams.query });
        obj.$promise.then(function (data) {
            vm.authors = data.authors;
            vm.totalItems = data.totalCount;
            vm.totalPages = Math.ceil(vm.totalCount / vm.pageCount);
            vm.maxSize = 5;
        })

        vm.pageChanged = function () {
            $location.search({ "pageCount": vm.pageCount, "pageNumb": vm.currPage });
        };
    }
})();