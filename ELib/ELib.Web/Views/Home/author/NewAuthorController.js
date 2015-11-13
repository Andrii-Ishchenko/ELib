(function () {
    angular.module("elib")
           .controller("NewAuthorController", NewAuthorController);

    NewAuthorController.$inject = ["authServiceFactory", "dataServiceFactory", "$location", "$timeout", 'STATE'];

    function NewAuthorController(authServiceFactory, dataServiceFactory, $location, $timeout, STATE) {
        var vm = this;

        authServiceFactory.fillAuthData();
        
        if (!authServiceFactory.authentication.isAuth) {
            $location.path(STATE.LOGIN);
        }

        vm.message = '';
        vm.submitState = false;
        vm.createdSuccessfully = false;

        vm.createAuthor = function () {
            vm.submitState = true;

            var author = {
                FirstName: vm.firstName,
                LastName: vm.lastName,
                Description: vm.description,
                DateOfBirth: vm.dateOfBirth,
                DeathDate: vm.deathDate,
                State : 0
            }                       

            dataServiceFactory.getService('authors').save(author).$promise.then(
                 //success
                 function (value) {
                     vm.createdSuccessfully = true;
                     vm.message = STATE.CREATION_SUCCESFUL;
                     startTimer(value.Id);
                 },
                 //error
                 function (error) {
                     console.log(error);
                     vm.submitState = false;
                     vm.message = STATE.CREATION_FAILED;
                 }
            );
        };

        var startTimer = function (Id) {
            var timer = $timeout(function () {
                $timeout.cancel(timer);
                $location.path(STATE.AUTHORS + Id);
            }, 2000);
        };
    }
})();