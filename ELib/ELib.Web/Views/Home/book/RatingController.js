(function () {
    angular.module("elib").controller('RatingController', RatingController)

    RatingController.$inject = ["dataServiceFactory", "CurrentProfileFactory"];

    function RatingController(dataServiceFactory, CurrentProfileFactory) {

        var vm = this;
             
        vm.basedOn = 100;
        vm.starsCount = 5;
        vm.iconClass = 'fa fa-star';
        vm.showGrade = true;
        vm.editableRating = true;

    }

})();
