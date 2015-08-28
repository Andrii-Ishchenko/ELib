(function () {
    angular.module("elib")
           .controller("ProfileController", ProfileController);

    ProfileController.$inject = ["ProfileFactory","$scope"];

    function ProfileController(ProfileFactory,$scope) {
        var vm = this;

        $scope.template = {
            menu: "/views/home/user/profile-menu.html",
            main: "/views/home/user/profile.html"
        }

        vm.profile = ProfileFactory.getCurrentUser().query();
        //console.log("profile: "+vm.profile);
    }

})();