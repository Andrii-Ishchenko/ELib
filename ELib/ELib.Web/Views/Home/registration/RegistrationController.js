(function () {
    angular.module("elib")
           .controller("RegistrationController", RegistrationController);

    RegistrationController.$inject = ["authServiceFactory", '$location', '$timeout', 'REGISTR_CONST'];

    function RegistrationController(authServiceFactory, $location, $timeout, REGISTR_CONST) {
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
                vm.message = REGISTR_CONST.SAVE_SUCCESSFULLY;
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
                     vm.message = REGISTR_CONST.ERROR + errors.join(' ');
                 }
                 else {
                     vm.message = REGISTR_CONST.SAVE_FAILED;
                 }                 
             });
        };

        var startTimer = function () {
            var timer = $timeout(function () {
                $timeout.cancel(timer);
                $location.path(REGISTR_CONST.LOGIN);
            }, 2000);
        }
    }
})();