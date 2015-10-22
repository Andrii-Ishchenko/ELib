(function () {
    angular.module("elib")
           .controller("CurrentProfileController", CurrentProfileController);

    CurrentProfileController.$inject = ["currentProfileFactory","dataServiceFactory","fileFactory", "$location"];

    function CurrentProfileController(currentProfileFactory,dataServiceFactory,fileFactory, $location) {
        var vm = this;
        vm.links = {
            "GeneralInfo": "/profile",
            "Ratings": "/profile/ratings",
            "Comments": '/profile/comments',
            "Favourites": "/profile/favs",
            "BooksToRead": "/profile/books/wishlist",
            "AlreadyReadBooks": "/profile/books/donelist",
            "SocialNetworks": "/profile/social-networks"
        };

        vm.files = "";
       
        
        vm.isActive = function (viewName) {
            var item = vm.links[viewName];
           // console.log("name = " + viewName + "\t" + "item = " + item);
            var result = ($location.path() == item);
            return result;
        }
        
        vm.isEditMode = false;

        vm.ToggleEditMode = function () {
            if (vm.isEditMode) {
            //post
            //in done() put ($scope.isEditMode = !$scope.isEditMode;)
            }
            vm.isEditMode = !vm.isEditMode;
        }
        vm.uploadProfileImage = function (file) {
            var fd = new FormData();
            fd.append("file", file[0]);
            fileFactory.uploadProfileImage(fd).then(
                function (response) {
                    vm.fetchData();
                    console.log(" method 'then' in post request.")
                });                      
        }

       vm.edit = function(){
            vm.backup = angular.copy(vm.profile);
        }

        vm.cancel = function(){
            vm.profile = vm.backup;
        }

        vm.save = function(){
            //save logic
            var person = {
                FirstName: vm.profile.FirstName,
                LastName: vm.profile.LastName,
                Email: vm.profile.Email,
            }

            dataServiceFactory.getService("CurrentProfile").update(person);
            //  CurrentProfileFactory.saveCurrentUser(vm.profile).send();          
        }

        vm.fetchData = function() {          
            vm.profile = currentProfileFactory.getCurrentUser().query();         
        }

        vm.fetchData();
        
    }

})();