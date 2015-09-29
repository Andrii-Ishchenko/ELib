(function () {
    angular.module("elib")
           .controller("CurrentProfileController", CurrentProfileController);

    CurrentProfileController.$inject = ["currentProfileFactory","dataServiceFactory","fileFactory"];

    function CurrentProfileController(currentProfileFactory,dataServiceFactory,fileFactory) {
        var vm = this;
        console.log(vm.person);
        vm.links = {
            "GeneralInfo": "/views/home/user/profile-general.html",
            "Ratings": "/views/home/user/profile-ratings.html",
            "Comments": '/views/home/user/profile-comments.html',
            "Favourites": "/views/home/user/profile-favs.html",
            "BooksToRead": "/views/home/user/profile-books-to-read.html",
            "AlreadyReadBooks": "/views/home/user/profile-already-read-books.html",
            "SocialNetworks": "/views/home/user/profile-social-networks.html"
        };

        vm.currUrl = vm.links["GeneralInfo"];
        vm.files = "";
        
        vm.showSection = function (name) {
            vm.currUrl = vm.links[name];
        }
        
        vm.isActive = function (viewName) {
            var item = vm.links[viewName];
           // console.log("name = " + viewName + "\t" + "item = " + item);
            var result = (vm.currUrl == item);
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