(function () {
    angular.module("elib")
           .controller("ProfileController", ProfileController);

    ProfileController.$inject = ["ProfileFactory"];

    function ProfileController(ProfileFactory) {
        var vm = this;

        vm.links = {
            "GeneralInfo": "/views/home/user/profile-general.html"           
        }

        vm.currUrl = vm.links["GeneralInfo"];

        vm.showSection = function (name) {
            vm.currUrl.main = vm.links[name];
        }

        vm.isActive = function (viewName) {
            var item = vm.links[viewName];
            //console.log("name = "+viewName+"\t"+"item = "+item);
            var result = (vm.currUrl == item);
            return result;
        }

        vm.profile = ProfileFactory.getCurrentUser().query();
        //console.log("profile: "+vm.profile);
    }

})();