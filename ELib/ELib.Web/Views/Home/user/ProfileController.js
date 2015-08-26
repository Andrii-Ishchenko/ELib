(function () {
    angular.module("elib")
           .controller("ProfileController", ProfileController);

    ProfileController.$inject = ["ProfileFactory"];

    function ProfileController(ProfileFactory) {
        var vm = this;
        vm.profile = ProfileFactory.getCurrentUser().query();
        console.log("profile: "+vm.profile);
    }

})();