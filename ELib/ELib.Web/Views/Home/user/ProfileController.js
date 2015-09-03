(function () {
    angular.module("elib")
           .controller("ProfileController", ProfileController);

    ProfileController.$inject = ["ProfileFactory", "$scope"];

    function ProfileController(ProfileFactory,$scope) {
        var vm = this;
       
        $scope.template = {
            menu: "/views/home/user/profile-menu.html",
            main: "/views/home/user/profile-general.html"
        }

        $scope.links = {
            "GeneralInfo": "/views/home/user/profile-general.html"           
        }

        $scope.showSection = function (name) {
            $scope.template.main = $scope.links[name];
        }

        $scope.isActive = function (viewName) {
            var item = $scope.links[viewName];
            //console.log("name = "+viewName+"\t"+"item = "+item);
            var result = ($scope.template.main == item);
            return result;
        }

        vm.profile = ProfileFactory.getCurrentUser().query();
        //console.log("profile: "+vm.profile);
    }

})();