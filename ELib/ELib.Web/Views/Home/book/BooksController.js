(function () {
    angular.module("ELib")
           .controller("BooksController", BooksController);

    BooksController.$inject = ["DataServiceFactory"];

    function BooksController(DataServiceFactory) {
        var vm = this;
        vm.books = DataServiceFactory.getAll('books').query();

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