(function () {
    angular.module('elib').controller('ModalController', ModalController)

    ModalController.$inject = ["dataServiceFactory", "$modal"];

    function ModalController(dataServiceFactory, $modal) {

        var vm = this;
        vm.animationsEnabled = true;

        vm.open = function (id) {

            var modalInstance = $modal.open({
                animation: vm.animationsEnabled,
                templateUrl: 'myModalContent.html',
                controller: 'ModalInstanceController as modalinst',
                resolve: {
                    book: dataServiceFactory.getService("books").get({ id: id })
                }
            });
        };
    }
})();
