(function () {
    angular.module("elib")
           .controller("LoginController", LoginController);

    LoginController.$inject = ["authServiceFactory", '$location'];

    function LoginController(authServiceFactory, $location) {
        var vm = this;

        authServiceFactory.fillAuthData();

        vm.loginData = {
            userName: "",
            password: ""
        };

        vm.message = "";

        vm.login = function () {

            authServiceFactory.login(vm.loginData).then(function (response) {

                $location.path('/books');

            },
             function (err) {
                 vm.message = 'Login is failed';
             });
        };

    }
})();