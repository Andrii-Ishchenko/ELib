(function () {
    angular.module("elib")
           .controller("NewPublisherController", NewPublisherController);

    NewPublisherController.$inject = ["authServiceFactory", "dataServiceFactory", "$location", "$timeout", 'PUBLISH_CONST'];

    function NewPublisherController(authServiceFactory, dataServiceFactory, $location, $timeout, PUBLISH_CONST) {
        var vm = this;

        authServiceFactory.fillAuthData();
        
        if (!authServiceFactory.authentication.isAuth) {
            $location.path(PUBLISH_CONST.LOGIN);
        }

        vm.message = '';
        vm.submitState = false;
        vm.createdSuccessfully = false;

        vm.createPublisher = function () {
            vm.submitState = true;

            var publisher = {
                Name: vm.name,
                State : 0
            }

            dataServiceFactory.getService('publishers').save(publisher).$promise.then(
               //success
               function (value) {
                   vm.createdSuccessfully = true;
                   vm.message = PUBLISH_CONST.SUCCES;
                   startTimer(value.Id);
               },
               //error
               function (error) {
                   vm.submitState = false;
                   vm.message = PUBLISH_CONST.ERROR;
               }
          );
        };

        var startTimer = function (publisherId) {
            var timer = $timeout(function () {
                $timeout.cancel(timer);
                $location.path(PUBLISH_CONST.PUBLISHERS + publisherId);
            }, 2000);
        };
    }
})();