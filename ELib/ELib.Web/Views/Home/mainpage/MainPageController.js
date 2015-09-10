(function () {
    angular.module("elib")
           .controller("MainPageController", MainPageController);

    MainPageController.$inject = ["dataServiceFactory","bookRepository",'$routeParams'];

    function MainPageController(dataServiceFactory,bookRepository, $routeParams) {
        var vm = this;
        vm.pageCount = 6;
        vm.currPage = ($routeParams.pageNumb) ? $routeParams.pageNumb : 1;
        var obj = bookRepository.getBestRatingBooks().get({ pageCount: $routeParams.pageCount, pageNumb: $routeParams.pageNumb });
        var obj2 = bookRepository.getNewBooks().get({ pageCount: $routeParams.pageCount, pageNumb: $routeParams.pageNumb });
        obj.$promise.then(function (data) {
            vm.books = data.books;
            vm.totalCount = data.totalCount;
            vm.totalPages = Math.ceil(vm.totalCount / vm.pageCount);
            vm.pages = new Array(vm.totalPages);
        })
        obj2.$promise.then(function (data) {
            vm.newbooks = data.newbooks;
            vm.totalCount = data.totalCount;
            vm.totalPages = Math.ceil(vm.totalCount / vm.pageCount);
            vm.pages = new Array(vm.totalPages);
        })

       
    }
})();