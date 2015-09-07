(function () {
    angular.module("elib")
           .controller("UploadBookController", UploadBookController);

    UploadBookController.$inject = ["authServiceFactory", '$location'];

    function UploadBookController(authServiceFactory, $location) {
        var vm = this;

        authServiceFactory.fillAuthData();
        
        if (!authServiceFactory.authentication.isAuth) {
            $location.path('/login');
        }
    }
})();