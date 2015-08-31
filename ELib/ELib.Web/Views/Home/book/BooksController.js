(function () {
    angular.module("elib")
           .controller("BooksController", BooksController);

    BooksController.$inject = ["dataServiceFactory", "$scope"];

    function BooksController(dataServiceFactory,$scope) {
        var vm = this;

        $scope.template = {
            menu: "/views/shared/menu.html",
            main: "/views/home/book/books.html"
        }

        vm.books = dataServiceFactory.getService('books').query();

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