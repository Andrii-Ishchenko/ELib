(function () {
    angular.module("elib")
           .controller("NewBookController", NewBookController);

    NewBookController.$inject = ["authServiceFactory", "dataServiceFactory", "$location"];

    function NewBookController(authServiceFactory, dataServiceFactory,  $location) {
        var vm = this;

        authServiceFactory.fillAuthData();
        
        if (!authServiceFactory.authentication.isAuth) {
            $location.path('/login');
        }

        var languages = dataServiceFactory.getService('languages').query();

        vm.originalLanguages = languages;
        vm.publishLanguages = languages;

        //    vm.publishers = dataServiceFactory.getService('publishers').query();

        // need improvement
        var obj = dataServiceFactory.getService('publishers').get({ pageCount: 100, pageNumb: 1 });
        obj.$promise.then(function (data) {
            vm.publishers = data.publishers;
        })

        vm.subgenres = dataServiceFactory.getService('subgenres').query();
        
        vm.newBook = function () {

        }
    }
})();