(function () {
    angular.module("elib")
           .controller("PublishersController", PublishersController);

    PublishersController.$inject = ["dataServiceFactory", '$routeParams'];

    function PublishersController(dataServiceFactory, $routeParams) {
        var vm = this;
        vm.pageCount = 3;
        vm.currPage = ($routeParams.pageNumb) ? $routeParams.pageNumb : 1;
        var obj = dataServiceFactory.getService('publishers').get({ pageCount: $routeParams.pageCount, pageNumb: $routeParams.pageNumb, query: $routeParams.query });
        obj.$promise.then(function (data) {
            vm.publishers = data.publishers;
            vm.totalCount = data.totalCount;
            vm.totalPages = Math.ceil(vm.totalCount / vm.pageCount);
            vm.pages = new Array(vm.totalPages);
        })
    }
})();