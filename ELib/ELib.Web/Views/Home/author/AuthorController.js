﻿(function () {
    angular.module("elib")
           .controller("AuthorController", AuthorController);

    AuthorController.$inject = ["bookRepository", '$routeParams', "fileFactory", "$resource", 'AUTHOR_CONST'];

    function AuthorController(bookRepository, $routeParams, fileFactory, $resource, AUTHOR_CONST) {
        var vm = this;
        vm.instance = getService().get({ id: $routeParams.id });
        vm.books = bookRepository.getBooksForAuthor().query({ id: $routeParams.id });

        function getService() {
            var url = AUTHOR_CONST.AUTHOR;
            return $resource(url, { id: '@id' }, {
                update: {
                    method: 'PUT',
                    isArray: false
                }
            });
        }

       vm.uploadAuthorImage = function (file) {
            var fd = new FormData();
            fd.append("file", file[0]);

            FileFactory.uploadAuthorImage(fd, vm.instance.Id).then(
                function (response) {                   
                   // alert("uploaded");
                    vm.instance = getService().get({ id: $routeParams.id });
                });
        }
    }
})();