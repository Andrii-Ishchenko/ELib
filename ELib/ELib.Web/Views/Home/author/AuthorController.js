(function () {
    angular.module("elib")
           .controller("AuthorController", AuthorController);

    AuthorController.$inject = ["dataServiceFactory", '$routeParams', "fileFactory", "$resource", 'AUTHOR_CONST'];

    function AuthorController(dataServiceFactory, $routeParams, fileFactory, $resource, AUTHOR_CONST) {
        var vm = this;

        //gets author object
        vm.instance = dataServiceFactory.getService("authors").get({ id: $routeParams.id, property : "author" });

        // gets list of books wich were written by the given author
        vm.books = dataServiceFactory.getService("authors").query({ id: $routeParams.id, property: "books" });

        vm.uploadAuthorImage = function (file) {
            var fd = new FormData();
            fd.append("file", file[0]);

            FileFactory.uploadAuthorImage(fd, vm.instance.Id).then(
                function (response) {    
                    vm.instance = getService().get({ id: $routeParams.id });
                });
        }
    }
})();