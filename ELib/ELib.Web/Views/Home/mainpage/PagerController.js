(function () {
    angular.module('elib').controller('PagerController', PagerController)

    PagerController.$inject = ["bookRepository", '$scope', '$log'];

    function PagerController(bookRepository, $scope, $log) {

        $scope.totalItems = 5;
        $scope.itemsPerPage = 2;
        $scope.currentPage = 1;

        
            var vm = this;
            vm.pageCount = 3;
            vm.books = bookRepository.getBestRatingBooks().query({ pageCount: 2, pageNumb: $scope.currentPage });
            //var obj = bookRepository.getBestRatingBooks().get({ pageCount: 2, pageNumb: $scope.currentPage });

            //obj.$promise.then(function (data) {

            //    vm.books = data.books;
            //    vm.totalCount = data.totalCount;
            //    vm.totalPages = Math.ceil(vm.totalCount / vm.pageCount);
            //    vm.pages = new Array(vm.totalPages);

            //})
        


        $scope.pageChanged = function () {
            $log.log('Page changed to: ' + $scope.currentPage);
            vm.books = bookRepository.getBestRatingBooks().query({ pageCount: 2, pageNumb: $scope.currentPage });

        };





    }
})();

