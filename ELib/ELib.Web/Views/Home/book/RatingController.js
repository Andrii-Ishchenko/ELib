(function () {
    angular.module("elib").controller('RatingController', RatingController)

    RatingController.$inject = ["dataServiceFactory", "authServiceFactory", 'BOOK_CONST'];

    function RatingController(dataServiceFactory, authServiceFactory, BOOK_CONST) {
        var vm = this;

        authServiceFactory.fillAuthData();

        if (authServiceFactory.authentication.isAuth) {
            vm.editableRating = true;
        }
        else { vm.editableRating = false; }
        
        vm.basedOn = BOOK_CONST.BASED_ON;
        vm.starsCount = BOOK_CONST.STARS_COUNT;
        vm.iconClass = BOOK_CONST.RATING_CLASS;
        vm.showGrade = true;  
    }
})();
