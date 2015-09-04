(function () {
    angular.module("elib")
           .controller("CurrentProfileController", CurrentProfileController);

    CurrentProfileController.$inject = ["CurrentProfileFactory","FileFactory", "$scope"];

    function CurrentProfileController(CurrentProfileFactory,FileFactory ,$scope) {
        var vm = this;

        $scope.template = {
            menu: "/views/home/user/profile-menu-current.html",
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
           // console.log("name = " + viewName + "\t" + "item = " + item);
            var result = ($scope.template.main == item);
            return result;
        }
        
        $scope.isEditMode = false;

        $scope.EditMode = function () {
            if ($scope.isEditMode) {
            //post
            //in done() put ($scope.isEditMode = !$scope.isEditMode;)
            }
            $scope.isEditMode = !$scope.isEditMode;
        }

        $scope.uploadProfileImage = function (file) {
            var fd = new FormData();
            fd.append("file", file[0]);

            FileFactory.uploadProfileImage(fd).then(
                function (response) {
                    $scope.fetchData();
                    console.log(" method 'then' in post request.")
                });;
            
            
        }

        $scope.fetchData = function() {
            vm.profile = CurrentProfileFactory.getCurrentUser().query();
        }

        $scope.fetchData();
        
    }

})();