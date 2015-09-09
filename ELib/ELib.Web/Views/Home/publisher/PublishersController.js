(function () {
    angular.module("elib")
           .controller("PublishersController", PublishersController);

    PublishersController.$inject = ["dataServiceFactory", "$scope", '$routeParams'];

    function PublishersController(dataServiceFactory, $scope, $routeParams) {
        var vm = this;

        $scope.template = {
            menu: "/views/shared/menu.html",
            main: "/views/home/publisher/publishers.html"
        }
        vm.publishers = dataServiceFactory.getService('publishers').query();

        vm.pageCount = 3;
        vm.currPage = ($routeParams.pageNumb) ? $routeParams.pageNumb : 1;
        var obj = dataServiceFactory.getService('publishers').get({ pageCount: $routeParams.pageCount, pageNumb: $routeParams.pageNumb });
        obj.$promise.then(function (data) {
            vm.publishers = data.publishers;
            vm.totalCount = data.totalCount;
            vm.totalPages = Math.ceil(vm.totalCount / vm.pageCount);
            vm.pages = new Array(vm.totalPages);
        })
    }
})();