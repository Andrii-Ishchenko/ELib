(function () {
    angular.module("elib")
           .controller("AuthorsController", AuthorsController);

    AuthorsController.$inject = ["dataServiceFactory", "$scope", '$routeParams', "$location"];

    function AuthorsController(dataServiceFactory, $scope, $routeParams, $location) {
        var vm = this;

        $scope.template = {
            menu: "/views/shared/menu.html",
            main: "/views/home/author/authors.html"
        }

        var obj = dataServiceFactory.getService('authors').get({ pageCount: $routeParams.pageCount, pageNumb: $routeParams.pageNumb, query : $routeParams.query });
        obj.$promise.then(function (data) {
            vm.authors = data.authors;
            vm.totalItems = data.totalCount;
            vm.itemsPerPage = ($routeParams.pageCount) ? $routeParams.pageCount : 5;
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