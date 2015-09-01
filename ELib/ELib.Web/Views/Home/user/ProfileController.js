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
            "GeneralInfo": "/views/home/user/profile-general.html",
            "Ratings": "/views/home/user/profile-ratings.html",
            "Comments": '/views/home/user/profile-comments.html',
            "Favourites": "/views/home/user/profile-favs.html",
            "BooksToRead": "/views/home/user/profile-books-to-read.html",
            "AlreadyReadBooks": "/views/home/user/profile-already-read-books.html",
            "SocialNetworks": "/views/home/user/profile-social-networks.html"
        }

        $scope.showSection = function (name) {
            $scope.template.main = $scope.links[name];
        }

        $scope.isActive = function (viewName) {
            var item = $scope.links[viewName];
            console.log("name = "+viewName+"\t"+"item = "+item);
            var result = ($scope.template.main == item);
            return result;
        }

        //$scope.showGeneralInfo = function () {
        //    $scope.template.main = "/views/home/user/profile-general.html";
        //}

        //$scope.showRatings = function () {
        //    $scope.template.main = "/views/home/user/profile-ratings.html";
        //}

        //$scope.showComments = function () {
        //    $scope.template.main = "/views/home/user/profile-comments.html";
        //}

        //$scope.showFavs = function () {
        //    $scope.template.main = "/views/home/user/profile-favs.html";
        //}

        //$scope.showBooksToRead = function () {
        //    $scope.template.main = "/views/home/user/profile-books-to-read.html";
        //}

        //$scope.showAlreadyReadBooks = function () {
        //    $scope.template.main = "/views/home/user/profile-already-read-books.html";
        //}

        //$scope.showSocialNetworks = function () {
        //    $scope.template.main = "/views/home/user/profile-social-networks.html";
        //}



        vm.profile = ProfileFactory.getCurrentUser().query();
        //console.log("profile: "+vm.profile);
    }

})();