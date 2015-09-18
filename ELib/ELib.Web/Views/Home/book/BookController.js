(function () {
    angular.module("elib")
           .controller("BookController", BookController);

    BookController.$inject = ["bookRepository", "commentRepository", '$routeParams', "FileFactory", "$scope"];

    function BookController(bookRepository, commentRepository, $routeParams, FileFactory, $scope) {
        var vm = this;

        vm.savedSuccessfully = false;
        vm.message = "";

        vm.instance = bookRepository.getBookById().get({ id: $routeParams.id });
        vm.comments = commentRepository.getCommentsByBookId().get({ id: $routeParams.id});
        vm.getFullStarsArray = function () {
            var fullStarsNumb = parseInt(vm.instance.Rating);
            var arr = [];
            if (fullStarsNumb > 0) arr.length = fullStarsNumb;
            return arr;
        };

        vm.getEmptyStarsArray = function () {
            var EmptyStarsNumb = 10 - parseInt(vm.instance.Rating);
            var arr = [];
            if (EmptyStarsNumb > 0) arr.length = EmptyStarsNumb;
            return arr;
        };

        $scope.uploadBookImage = function (file) {
            var fd = new FormData();
            fd.append("file", file[0]);

            FileFactory.uploadBookImage(fd,vm.instance.Id).then(
                function (response) {
                    // $scope.fetchData();
                   // alert("uploaded");
                    vm.instance = bookRepository.getBookById().get({ id: $routeParams.id });
                });
        }


        $scope.uploadBookFile = function (file) {
            var fd = new FormData();
            fd.append("file", file[0]);

            FileFactory.uploadBookFile(fd, $routeParams.id).then(
                function (response) {
                    vm.savedSuccessfully = true;
                    vm.message = "Book file has been uploaded successfully";
                },
                function (error) {
                    vm.message = "Book file uploading is failed";
                });
        };
    }
})();