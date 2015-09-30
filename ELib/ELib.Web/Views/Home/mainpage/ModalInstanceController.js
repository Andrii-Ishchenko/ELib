(function () {
    angular.module('elib').controller('ModalInstanceController', ModalInstanceController)

    ModalInstanceController.$inject = [ "$modalInstance", "book"];

    function ModalInstanceController( $modalInstance, book) {
        var vm = this;
        vm.book = book;

        vm.ok = function () {
            $modalInstance.close();
        };
    }
})();