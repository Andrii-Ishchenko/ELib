(function () {
    angular.module("elib")
           .controller("ProfileController", ProfileController);

    ProfileController.$inject = ["ProfileFactory", "$scope"];

    function ProfileController(ProfileFactory,$scope) {
        var vm = this;

        $scope.showGeneralInfo = function () {
            $scope.template.main = "/views/home/user/profile-general.html";
        }

        $scope.showRatings = function () {
            $scope.template.main = "/views/home/user/profile-ratings.html";
        }

        $scope.showComments = function () {
            $scope.template.main = "/views/home/user/profile-comments.html";
        }

        $scope.showFavs = function () {
            $scope.template.main = "/views/home/user/profile-favs.html";
        }

        $scope.showBooksToRead = function () {
            $scope.template.main = "/views/home/user/profile-books-to-read.html";
        }

        $scope.showAlreadyReadBooks = function () {
            $scope.template.main = "/views/home/user/profile-already-read-books.html";
        }

        $scope.showSocialNetworks = function () {
            $scope.template.main = "/views/home/user/profile-social-networks.html";
        }

        $scope.template = {
            menu: "/views/home/user/profile-menu.html",
            main: "/views/home/user/profile-general.html"
        }

        vm.profile = ProfileFactory.getCurrentUser().query();
        //console.log("profile: "+vm.profile);
    }

})();