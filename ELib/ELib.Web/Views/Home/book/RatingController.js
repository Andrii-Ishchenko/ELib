(function () {
    angular.module("elib").controller('RatingController', RatingController)

    RatingController.$inject = ["dataServiceFactory", "CurrentProfileFactory", "authServiceFactory"];

    function RatingController(dataServiceFactory, CurrentProfileFactory, authServiceFactory) {


        var vm = this;

        authServiceFactory.fillAuthData();

        if (authServiceFactory.authentication.isAuth) {
            vm.editableRating = true;
        }
        else { vm.editableRating = false; }
        
        vm.basedOn = 100;
        vm.starsCount = 5;
        vm.iconClass = 'fa fa-star';
        vm.showGrade = true;
        

    }

})();
