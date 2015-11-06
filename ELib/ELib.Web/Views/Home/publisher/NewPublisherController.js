(function () {
    angular.module("elib")
           .controller("NewPublisherController", NewPublisherController);

    NewPublisherController.$inject = ["authServiceFactory", "dataServiceFactory", "$location", "$timeout"];

    function NewPublisherController(authServiceFactory, dataServiceFactory, $location, $timeout) {
        var vm = this;

        //authServiceFactory.fillAuthData();
        
       // if (!authServiceFactory.authentication.isAuth) {
       //     $location.path('/login');
       // }

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
                   vm.message = "Publisher has been created successfully, you will be redicted to publisher page in 2 seconds.";
                   startTimer(value.Id);
               },
               //error
               function (error) {
                   vm.submitState = false;
                   vm.message = "Publisher creation is failed";
               }
          );
        };

        var startTimer = function (publisherId) {
            var timer = $timeout(function () {
                $timeout.cancel(timer);
                $location.path('/publishers/' + publisherId);
            }, 2000);
        };
    }
})();