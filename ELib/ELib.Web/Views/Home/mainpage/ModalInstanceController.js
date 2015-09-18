(function () {
    angular.module('elib').controller('ModalInstanceController', ModalInstanceController)

    ModalInstanceController.$inject = ["$scope", "$modalInstance", "book"];

    function ModalInstanceController($scope, $modalInstance, book) {
        var vm = this;
        vm.book = book;

        vm.ok = function () {
            $modalInstance.close();
        };
    }
})();