(function () {
    angular.module("Elib")
           .controller("BooksController", BooksController);

    BooksController.$inject = ["DataService"];

    function BooksController(DataService) {
        var vm = this;
        vm.books = [];

        activate();

        function activate() {
            return getBooks().then(function () {
                logger.info('Activated Books View');
            });
        }

        function getBooks() {
            return DataService.getAll('book')
            .then(function (data) {
                vm.books = data;
                return vm.books;
            });
        }
    }
})();