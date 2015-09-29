(function () {
    angular.module("elib")
           .controller("BookController", BookController);

    BookController.$inject = ["bookRepository", "CommentsRepository", '$routeParams', "FileFactory", "$scope","dataServiceFactory"];

    function BookController(bookRepository, CommentsRepository, $routeParams, FileFactory, $scope,dataServiceFactory) {
        var vm = this;

        vm.savedSuccessfully = false;
        vm.message = "";

        vm.instance = bookRepository.getBookById().get({ id: $routeParams.id });
        vm.comments = CommentsRepository.getCommentsByBookId().get({ id: $routeParams.id});

        vm.newComment = {
            Text: "Hello World!!!",
            BookId: $routeParams.id
        };

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


        vm.createComment = function () {

        };

        vm.clean = function () {
            vm.newComment.Text = "";
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

        $scope.postComment = function (text) {
            CommentsRepository.S
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