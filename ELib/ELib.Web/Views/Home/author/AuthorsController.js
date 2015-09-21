(function () {
    angular.module("elib")
           .controller("AuthorsController", AuthorsController);

    AuthorsController.$inject = ["dataServiceFactory", '$routeParams', "$location"];

    function AuthorsController(dataServiceFactory, $routeParams, $location) {
        var vm = this;
        var obj = dataServiceFactory.getService('authors').get({ pageCount: $routeParams.pageCount, pageNumb: $routeParams.pageNumb, query : $routeParams.query });
        obj.$promise.then(function (data) {
            vm.authors = data.authors;
            vm.totalItems = data.totalCount;
            vm.itemsPerPage = ($routeParams.pageCount) ? $routeParams.pageCount : 2;
            vm.currentPage = ($routeParams.pageNumb) ? $routeParams.pageNumb : 1;
            
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