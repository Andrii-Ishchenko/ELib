(function () {
    angular.module('elib').controller('MainController', MainController)

    MainController.$inject = ["bookRepository"];

    function MainController(bookRepository) {

        var vm = this;
        vm.itemsPerPage = 2;

        vm.currentPage = 1;
        vm.currentPageNew = 1;

        var obj = bookRepository.getBestRatingBooks().get({ pageCount: vm.itemsPerPage, pageNumb: vm.currentPage });

        obj.$promise.then(function (data) {
            vm.books = data.books;
            vm.totalItems = data.totalCount;
        })

        vm.pageChanged = function () {
            obj = bookRepository.getBestRatingBooks().get({ pageCount: vm.itemsPerPage, pageNumb: vm.currentPage });

            obj.$promise.then(function (data) {
                vm.books = data.books;
            })
        };


        var obj2 = bookRepository.getNewBooks().get({ pageCount: vm.itemsPerPage, pageNumb: vm.currentPageNew });

        obj2.$promise.then(function (data) {
            vm.booksNew = data.newbooks;
            vm.totalItemsNew = data.totalCount;
        })

        vm.pageChangedNew = function () {
            obj2 = bookRepository.getNewBooks().get({ pageCount: vm.itemsPerPage, pageNumb: vm.currentPageNew });

            obj2.$promise.then(function (data) {
                vm.booksNew = data.newbooks;
            })
        };
    }
})();

