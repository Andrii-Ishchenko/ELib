(function () {
    angular.module("elib")
           .controller("ProfileController", ProfileController);

    ProfileController.$inject = ["ProfileFactory","$scope"];

    function ProfileController(ProfileFactory,$scope) {
        var vm = this;

        vm.profile = ProfileFactory.getCurrentUser().query();
        //console.log("profile: "+vm.profile);
    }

})();