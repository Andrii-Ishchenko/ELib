(function (){
    angular.module('elib').controller('PagerController',PagerController) 
   
    PagerController.$inject = ["bookRepository",'$scope', '$log'];

    function PagerController(bookRepository,$scope, $log) {
        
        var vm = this;
        vm.pageCount = 3;
           var obj = bookRepository.getBestRatingBooks().get({ pageCount: 3, pageNumb: 1 });

        obj.$promise.then(function (data) {
            vm.books = data.books;
            vm.totalCount = data.totalCount;
            vm.totalPages = Math.ceil(vm.totalCount / vm.pageCount);
            vm.pages = new Array(vm.totalPages);
        })


            $scope.totalItems = 64;
            $scope.currentPage = 1;

            $scope.setPage = function (pageNo) {
                $scope.currentPage = pageNo;
            };

            $scope.pageChanged = function () {
                $log.log('Page changed to: ' + $scope.currentPage);
            };

            $scope.maxSize = 5;
            $scope.bigTotalItems = 175;
            $scope.bigCurrentPage = 1;
        }
})();

