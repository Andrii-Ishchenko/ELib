(function () {
    angular.module("elib")
           .controller("LoginController", LoginController);

    LoginController.$inject = ["authServiceFactory", '$location', 'LOGIN_CONST'];

    function LoginController(authServiceFactory, $location, LOGIN_CONST) {
        var vm = this;

        authServiceFactory.fillAuthData();

        vm.loginData = {
            userName: "",
            password: ""
        };

        vm.message = "";

        vm.login = function () {

            authServiceFactory.login(vm.loginData).then(function (response) {
                $location.path(LOGIN_CONST.BOOKS );
            },
             function (err) {
                 vm.message = LOGIN_CONST.ERROR;
             });
        };
    }
})();