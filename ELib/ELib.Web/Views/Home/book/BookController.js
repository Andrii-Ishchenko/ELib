(function () {
    angular.module("elib")
           .controller("BookController", BookController);

    BookController.$inject = ["bookRepository", "CommentsRepository", '$routeParams', "fileFactory", "$scope", "authServiceFactory", "dataServiceFactory", "currentProfileFactory"];

    function BookController(bookRepository, CommentsRepository, $routeParams, fileFactory, $scope, authServiceFactory, dataServiceFactory, currentProfileFactory) {
        var vm = this;

        vm.savedSuccessfully = false;
        vm.message = "";

        vm.instance = bookRepository.getBookById().get({ id: $routeParams.id });
        vm.profile = currentProfileFactory.getCurrentUser().query();
        var userCurrId = vm.profile.Id;
        //vm.getFullStarsArray = function () {
        //    var fullStarsNumb = parseInt(vm.instance.Rating);
        //    var arr = [];
        //    if (fullStarsNumb > 0) arr.length = fullStarsNumb;
        //    return arr;
        //};

        //vm.getEmptyStarsArray = function () {
        //    var EmptyStarsNumb = 10 - parseInt(vm.instance.Rating);
        //    var arr = [];
        //    if (EmptyStarsNumb > 0) arr.length = EmptyStarsNumb;
        //    return arr;
        //};

        vm.changeRating = function () {
            createRating();
        };
        
        vm.comments = CommentsRepository.getCommentsByBookId().get({ id: $routeParams.id });

        vm.newComment = {
            Text: "abc",
            BookId: $routeParams.id,
            UserId: null,
            CommentDate: null,
            SumLike: 0,
            SumDisLike: 0
        };

        vm.cleanComment = function () {
            vm.newComment.Text = "";
        };


        var createRating = function () {
                vm.submitState = true;
                var rating = {
                        ValueRating: vm.instance.Rating,
                        UserId: vm.profile.Id,
                        BookId: parseInt($routeParams.id)
                }
                dataServiceFactory.getService('Ratings').save(rating).$promise.then(
                  //success
                  function (value) {
                      vm.instance = bookRepository.getBookById().get({ id: $routeParams.id });
                }

             );
        };


        vm.createComment = function () {
            vm.newComment.CommentDate = new Date();
            var userIdCmnt = vm.profile.Id;
            vm.newComment.UserId = userIdCmnt;
            dataServiceFactory.getService('Comments').save(vm.newComment);


            location.reload();

        };



            vm.uploadBookImage = function (file) {
                var fd = new FormData();
                fd.append("file", file[0]);

                fileFactory.uploadBookImage(fd, vm.instance.Id).then(
                    function (response) {
                        // $scope.fetchData();
                        // alert("uploaded");
                        vm.instance = bookRepository.getBookById().get({ id: $routeParams.id });
            });
        }

            $scope.postComment = function (text) {
                CommentsRepository.S
        }


            vm.uploadBookFile = function (file) {
                var fd = new FormData();
                fd.append("file", file[0]);

                fileFactory.uploadBookFile(fd, $routeParams.id).then(
                    function (response) {
                        vm.savedSuccessfully = true;
                        vm.message = "Book file has been uploaded successfully";
                        // need refactoring (get only book instances)
                        var bookInstance = bookRepository.getBookById().get({ id: $routeParams.id })
                            .$promise
                            .then(
                                function (response) {
                                    vm.instance.BookInstances = response.BookInstances;
                    });
                },
                    function (error) {
                        vm.message = "Book file uploading is failed";
            });
            };
        }
})();