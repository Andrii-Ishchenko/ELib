(function () {
    angular.module("elib")
           .controller("NewAuthorController", NewAuthorController);

    NewAuthorController.$inject = ["authServiceFactory", "dataServiceFactory", "$location", "$timeout"];

    function NewAuthorController(authServiceFactory, dataServiceFactory, $location, $timeout) {
        var vm = this;

        //authServiceFactory.fillAuthData();
        
       // if (!authServiceFactory.authentication.isAuth) {
       //     $location.path('/login');
       // }

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
                DeathDate: vm.deathDate
            }                       

            dataServiceFactory.getService('authors').save(author).$promise.then(
                 //success
                 function (value) {
                     vm.createdSuccessfully = true;
                     vm.message = "Author has been created successfully, you will be redicted to author page in 2 seconds.";
                     startTimer(value.Id);
                 },
                 //error
                 function (error) {
                     console.log(error);
                     vm.submitState = false;
                     vm.message = "Author creation is failed";
                 }
            );
        };

        var startTimer = function (Id) {
            var timer = $timeout(function () {
                $timeout.cancel(timer);
                $location.path('/authors/' + Id);
            }, 2000);
        };
    }
})();