(function () {
    angular.module("elib")
           .controller("RegistrationController", RegistrationController);

    RegistrationController.$inject = ["authServiceFactory", '$location', '$timeout'];

    function RegistrationController(authServiceFactory, $location, $timeout) {
        var vm = this;

        authServiceFactory.fillAuthData()
        vm.savedSuccessfully = false;
        vm.message = "";

        vm.registration = {
            userName: "",
            email: "",
            password: ""
        };

        vm.register = function () {
            authServiceFactory.saveRegistration(vm.registration).then(function (response) {

                vm.savedSuccessfully = true;
                vm.message = "User has been registered successfully, you will be redicted to login page in 2 seconds.";
                startTimer();

            },
             function (response) {
                 var errors = [];
                 for (var key in response.data.modelState) {
                     for (var i = 0; i < response.data.modelState[key].length; i++) {
                         errors.push(response.data.modelState[key][i]);
                     }
                 }
                 if (errors.length > 0) {
                     vm.message = "Failed to register user due to:" + errors.join(' ');
                 }
                 else {
                     vm.message = "Registration is failed";
                 }
                 
             });
        };

        var startTimer = function () {
            var timer = $timeout(function () {
                $timeout.cancel(timer);
                $location.path('/login');
            }, 2000);
        }

    }
})();