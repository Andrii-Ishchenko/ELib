﻿(function () {
    angular.module("elib")
           .controller("CurrentProfileController", CurrentProfileController);

    CurrentProfileController.$inject = ["dataServiceFactory", "fileFactory", "$location", 'USER_CONST'];

    function CurrentProfileController(dataServiceFactory, fileFactory, $location, USER_CONST) {
        var vm = this;
        vm.links = {
            "GeneralInfo": USER_CONST.PROFILE ,
            "Ratings": USER_CONST.RATINGS ,
            "Comments": USER_CONST.COMMENTS ,
            "Favourites": USER_CONST.FAVORITES ,
            "BooksToRead": USER_CONST.WISHLIST ,
            "AlreadyReadBooks": USER_CONST.DONELIST ,
            "SocialNetworks": USER_CONST.SOTIAL_NETWORKS
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
                State : 1
            }

            dataServiceFactory.getService("CurrentProfile").update(person);         
        }

        vm.fetchData = function() {          
            vm.profile = dataServiceFactory.getService("CurrentProfile").get();
        }

        vm.fetchData();        
    }
})();