(function () {
    angular.module("elib")
           .controller("BookController", BookController);

    BookController.$inject = ["bookRepository", "CommentsRepository", '$routeParams', "fileFactory", "$scope", "authServiceFactory", "dataServiceFactory", "currentProfileFactory"];

    function BookController(bookRepository, CommentsRepository, $routeParams, fileFactory, $scope, authServiceFactory, dataServiceFactory, currentProfileFactory) {
        var vm = this;

        vm.IsPostEnabled = true;

        vm.CanPost = function () {
            if (vm.IsPostEnabled && vm.newComment.Text.length > 0 && vm.newComment.Text.length < 400)
                return true;
            else
                return false;
        };


        vm.savedSuccessfully = false;
        vm.message = "";

        bookRepository.getBookById().get({ id: $routeParams.id }).$promise.then(function (data) {
            vm.instance = data;
        });
        vm.profile = currentProfileFactory.getCurrentUser().query();
        var userCurrId = vm.profile.Id;

        vm.changeRating = function () {
            createRating();
        };
        
        vm.comments = CommentsRepository.getCommentsByBookId().get({ id: $routeParams.id });

        vm.newComment = {
            Text: "",
            BookId: $routeParams.id,
            UserId: null,
            CommentDate: null,
            SumLike: 0,
            SumDisLike: 0,
            UserName: ""
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
            vm.IsPostEnabled = false;
            vm.newComment.CommentDate = new Date();
            var userIdCmnt = vm.profile.Id;
            vm.newComment.UserId = userIdCmnt;
            vm.newComment.UserName = vm.profile.UserName;
            dataServiceFactory.getService('Comments').save(vm.newComment).$promise.then(
                 //success
                 function (value) {
                     vm.comments = CommentsRepository.getCommentsByBookId().get({ id: $routeParams.id });
                     vm.newComment.Text = "";
                     vm.IsPostEnabled = true;
                 },
                 //error
                 function (error) {
                     vm.newComment.Text = "";
                     vm.IsPostEnabled = true;
                 }
            );
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