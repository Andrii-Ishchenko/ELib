angular.module('elib').controller('ModalDemoCtrl', function (bookRepository,$scope, $modal, $log) {

    //$scope.book = bookRepository.getBookById().get({ id: 1 });
    $scope.animationsEnabled = true;

    $scope.open = function (id) {

        var modalInstance = $modal.open({
            animation: $scope.animationsEnabled,
            templateUrl: 'myModalContent.html',
            controller: 'ModalInstanceCtrl',
            resolve: {
                book: bookRepository.getBookById().get({ id: id })
                //    function () {
                //    return $scope.book;
                //}
            }
        });

       
    };

    

});

// Please note that $modalInstance represents a modal window (instance) dependency.
// It is not the same as the $modal service used above.

angular.module('elib').controller('ModalInstanceCtrl', function ($scope, $modalInstance, book) {
    $scope.book=book
    
    $scope.ok = function () {
        //$modalInstance.close($scope.selected.item);
        $modalInstance.close();
    };
   
});