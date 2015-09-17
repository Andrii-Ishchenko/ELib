(function () {
    angular.module('elib').controller('MainController', MainController)

    MainController.$inject = ["dataServiceFactory"];

    function MainController(dataServiceFactory) {

        var vm = this;
        vm.itemsPerPage = 2;

        vm.currentPage = 1;
        vm.currentPageNew = 1;

        var obj = dataServiceFactory.getService('books').get({ blockId: 0, pageCount: vm.itemsPerPage, pageNumb: vm.currentPage });

        obj.$promise.then(function (data) {
            vm.books = data.books;
            vm.totalItems = data.totalCount;
        })

        vm.pageChanged = function () {
            obj = dataServiceFactory.getService('books').get({ blockId: 0, pageCount: vm.itemsPerPage, pageNumb: vm.currentPage });

            obj.$promise.then(function (data) {
                vm.books = data.books;
            })
        };


        var obj2 = dataServiceFactory.getService('books').get({ blockId: 1, pageCount: vm.itemsPerPage, pageNumb: vm.currentPageNew });

        obj2.$promise.then(function (data) {
            vm.booksNew = data.books;
            vm.totalItemsNew = data.totalCount;
        })

        vm.pageChangedNew = function () {
            obj2 = dataServiceFactory.getService('books').get({ blockId: 1, pageCount: vm.itemsPerPage, pageNumb: vm.currentPageNew });

            obj2.$promise.then(function (data) {
                vm.booksNew = data.books;
            })
        };
    }
})();

