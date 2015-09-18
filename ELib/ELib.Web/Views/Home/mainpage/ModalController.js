(function () {
    angular.module('elib').controller('ModalController', ModalController)

    ModalController.$inject = ["bookRepository", "$modal"];

    function ModalController(bookRepository, $modal) {

        var vm = this;
        vm.animationsEnabled = true;

        vm.open = function (id) {

            var modalInstance = $modal.open({
                animation: vm.animationsEnabled,
                templateUrl: 'myModalContent.html',
                controller: 'ModalInstanceController as modalinst',
                resolve: {
                    book: bookRepository.getBookById().get({ id: id })
                }
            });
        };
    }
})();
