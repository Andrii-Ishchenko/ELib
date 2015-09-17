(function () {
    angular.module("elib")
           .controller("BooksController", BooksController);

    BooksController.$inject = ["dataServiceFactory", "$scope", '$routeParams'];

    function BooksController(dataServiceFactory,$scope, $routeParams) {
        var vm = this;

        $scope.template = {
            menu: "/views/home/book/book-list-menu.html",
            main: "/views/home/book/books.html"
        }
        vm.pageCount = $routeParams.pageCount;
        vm.currPage = ($routeParams.pageNumb) ? $routeParams.pageNumb : 1;
        var obj = dataServiceFactory.getService('books').get({ pageCount: $routeParams.pageCount, pageNumb : $routeParams.pageNumb, query : $routeParams.query});
        obj.$promise.then(function (data) {
            vm.books = data.books;
            vm.totalCount = data.totalCount;
            vm.totalPages = Math.ceil(vm.totalCount / vm.pageCount);
            vm.pages = new Array(vm.totalPages);
        })
      

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