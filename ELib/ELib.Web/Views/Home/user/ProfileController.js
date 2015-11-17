(function () {
    angular.module("elib")
           .controller("ProfileController", ProfileController);

    ProfileController.$inject = ["profileFactory", 'USER_CONST'];

    function ProfileController(profileFactory, USER_CONST) {
        var vm = this;

        vm.links = {
            "GeneralInfo": USER_CONST.GENERAL_INFO        
        }

        vm.currUrl = vm.links["GeneralInfo"];

        vm.showSection = function (name) {
            vm.currUrl.main = vm.links[name];
        }

        vm.isActive = function (viewName) {
            var item = vm.links[viewName];
            var result = (vm.currUrl == item);
            return result;
        }

        vm.profile = profileFactory.getCurrentUser().query();
    }
})();