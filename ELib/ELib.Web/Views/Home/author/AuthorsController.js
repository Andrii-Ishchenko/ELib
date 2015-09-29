(function () {
    angular.module("elib")
           .controller("AuthorsController", AuthorsController);

    AuthorsController.$inject = ["dataServiceFactory", '$routeParams', "$location"];

    function AuthorsController(dataServiceFactory, $routeParams, $location) {
        var vm = this;
        vm.itemsPerPage = ($routeParams.pageCount) ? $routeParams.pageCount : 5;
        vm.currentPage = ($routeParams.pageNumb) ? $routeParams.pageNumb : 1;
        var obj = dataServiceFactory.getService('authors').get({ pageCount: vm.itemsPerPage, pageNumb: vm.currentPage, query: $routeParams.query });
        obj.$promise.then(function (data) {
            vm.authors = data.authors;
            vm.totalItems = data.totalCount;            
            vm.totalPages = Math.ceil(vm.totalCount / vm.pageCount);
            vm.maxSize = 5;
        })

        vm.pageChanged = function () {
            $location.search("pageCount", vm.itemsPerPage);
            $location.search("pageNumb", vm.currentPage);
        };


        //activate();

        //function activate() {
        //    return getBooks().then(function () {
        //        console.log('Activated Books View');
        //    });
        //}

        //function getBooks() {
        //    return DataService.getAll('book')
        //    .then(function (data) {
        //        vm.books = data;
        //        console.log(vm.books);
        //        return vm.books;
        //    });
        //}
    }
})();