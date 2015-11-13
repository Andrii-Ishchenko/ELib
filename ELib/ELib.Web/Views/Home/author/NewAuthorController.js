(function () {
    angular.module("elib")
           .controller("NewAuthorController", NewAuthorController);

    NewAuthorController.$inject = ["authServiceFactory", "dataServiceFactory", "$location", "$timeout", 'AUTHOR_CONST'];

    function NewAuthorController(authServiceFactory, dataServiceFactory, $location, $timeout, AUTHOR_CONST) {
        var vm = this;

        authServiceFactory.fillAuthData();
        
        if (!authServiceFactory.authentication.isAuth) {
            $location.path(AUTHOR_CONST.LOGIN);
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
                     vm.message = AUTHOR_CONST.CREATION_SUCCESFUL;
                     startTimer(value.Id);
                 },
                 //error
                 function (error) {
                     console.log(error);
                     vm.submitState = false;
                     vm.message = AUTHOR_CONST.CREATION_FAILED;
                 }
            );
        };

        var startTimer = function (Id) {
            var timer = $timeout(function () {
                $timeout.cancel(timer);
                $location.path(AUTHOR_CONST.AUTHORS +Id);
            }, 2000);
        };
    }
})();