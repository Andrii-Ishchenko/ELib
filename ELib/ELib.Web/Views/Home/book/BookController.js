(function () {
    angular.module("elib")
           .controller("BookController", BookController);

    BookController.$inject = ["bookRepository", "CommentsRepository", '$routeParams', "fileFactory", "$scope", "authServiceFactory", "dataServiceFactory", "currentProfileFactory", 'BOOK_CONST'];

    function BookController(bookRepository, CommentsRepository, $routeParams, fileFactory, $scope, authServiceFactory, dataServiceFactory, currentProfileFactory, BOOK_CONST) {
        var vm = this;
        vm.isEditMode = false;
        vm.IsPostEnabled = true;
        vm.IsPostRefresh = true;
        vm.CanPost = function () {
            if (vm.IsPostEnabled && vm.newComment.Text.length > 0 && vm.newComment.Text.length < BOOK_CONST.MAX_TEXT)
                return true;
            else
                return false;
        };

        vm.CanLoad = function () {
            if (vm.temp.length < BOOK_CONST.LENGTH) {
                vm.IsPostRefresh = false;
                return true;
            }
            else
                return false;
        };

        vm.savedSuccessfully = false;
        vm.message = "";

        vm.instance = bookRepository.getBookById().get({ id: $routeParams.id });

        vm.profile = currentProfileFactory.getCurrentUser().query();
        var userCurrId = vm.profile.Id;

        vm.changeRating = function () {
            createRating();
        };

        var currentFetchedPageOfComments = BOOK_CONST.CURRENT_COMMENTS;
        var countOfFetchComments = BOOK_CONST.COUNT_COMMENTS;

        vm.comments = CommentsRepository.getCommentsByBookId().get({ id: $routeParams.id, pageCount: countOfFetchComments, pageNumb: currentFetchedPageOfComments });
        vm.temp = CommentsRepository.getCommentsByBookId().get({ id: $routeParams.id, pageCount: countOfFetchComments, pageNumb: currentFetchedPageOfComments });


        vm.newComment = {
            Text: "",
            BookId: $routeParams.id,
            UserId: null,
            CommentDate: null,
            SumLike: 0,
            SumDisLike: 0,
            UserName: "",
            State: "Added"
        };

        vm.cleanComment = function () {
            vm.newComment.Text = "";
        };

        vm.like = function (commentId) {
            createLikeOrDisLike(1,commentId);
        };
        vm.disLike = function (commentId) {
            createLikeOrDisLike(0,commentId);
        };

        vm.fetchComments = function () {
            currentFetchedPageOfComments = currentFetchedPageOfComments + 1;
            console.log(vm.comments.length);


            CommentsRepository.getCommentsByBookId().get({ id: $routeParams.id, pageCount: countOfFetchComments, pageNumb: currentFetchedPageOfComments }).$promise.then(
                 function (value) {
                     vm.temp = value;
                     var iterator = vm.temp.length;
                     for (var i = 0; i < iterator; i++) {
                         vm.comments.push(vm.temp[i]);
                     }
                 }
                );
            console.log(vm.temp);
            console.log(vm.comments);
            console.log(vm.temp.length);
            console.log(vm.CanLoad());
        };

        var createRating = function () {
            vm.submitState = true;
            var rating = {
                ValueRating: vm.instance.Rating,
                UserId: vm.profile.Id,
                BookId: parseInt($routeParams.id),
            }
            dataServiceFactory.getService('Ratings').save(rating).$promise.then(
              //success
              function (value) {
                  vm.instance = bookRepository.getBookById().get({ id: $routeParams.id });
              }
         );
        };

        var createLikeOrDisLike = function (isLike,commentId) {
            var ratingComment = {
                CommentId: commentId,
                UserId: vm.profile.Id,
                IsLike: isLike,
                State:"Added"
            }
            dataServiceFactory.getService('RatingsComment').save(ratingComment).$promise.then(
             //success
             function (value) {
                 vm.comments = CommentsRepository.getCommentsByBookId().get({ id: $routeParams.id });
                 vm.temp = vm.comments;
                 currentFetchedPageOfComments = BOOK_CONST.CURRENT_COMMENTS;
                 countOfFetchComments = BOOK_CONST.COUNT_COMMENTS;
                 vm.newComment.Text = "";
                 vm.IsPostEnabled = true;
                 vm.CanLoad();
             }
        );
        };

        vm.createComment = function () {
            vm.IsPostEnabled = false;
            vm.newComment.CommentDate = new Date();
            var userIdCmnt = vm.profile.Id;
            vm.newComment.UserId = userIdCmnt;
            vm.newComment.UserName = vm.profile.UserName;
            vm.newComment.State = 0;
            dataServiceFactory.getService('Comments').save(vm.newComment).$promise.then(
                 //success
                 function (value) {
                     vm.comments = CommentsRepository.getCommentsByBookId().get({ id: $routeParams.id });
                     vm.temp = vm.comments;
                     currentFetchedPageOfComments = BOOK_CONST.CURRENT_COMMENTS;
                     countOfFetchComments = BOOK_CONST.COUNT_COMMENTS;
                     vm.newComment.Text = "";
                     vm.IsPostEnabled = true;
                     vm.CanLoad();
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
                    vm.instance = bookRepository.getBookById().get({ id: $routeParams.id });
                });
        }

        vm.uploadBookFile = function (file) {
            var fd = new FormData();
            fd.append("file", file[0]);

            fileFactory.uploadBookFile(fd, $routeParams.id).then(
                function (response) {
                    vm.savedSuccessfully = true;
                    vm.message = BOOK_CONST.UPLOADING_SUCCESFUL;
                    // need refactoring (get only book instances)
                    var bookInstance = bookRepository.getBookById().get({ id: $routeParams.id })
                        .$promise
                        .then(
                            function (response) {
                                vm.instance.BookInstances = response.BookInstances;
                            });
                },
                function (error) {
                    vm.message = BOOK_CONST.UPLOADING_FAILED;
                });
        };

        vm.ToggleEditMode = function () {
            vm.isEditMode = !vm.isEditMode;
        }

        vm.edit = function () {
            vm.backup = angular.copy(vm.instance);

            dataServiceFactory.getService('authors').get(null).$promise.then(function (data) {
                vm.allAuthors = data.authors;
                for (var a = 0; a < vm.instance.Authors.length; a++) {
                    for (var i = 0; i < vm.allAuthors.length; i++) {
                        if (vm.instance.Authors[a].Id == vm.allAuthors[i].Id) {
                            vm.allAuthors.splice(i, 1);
                            break;
                        }
                    }
                }
            })
            dataServiceFactory.getService('publishers').get(null).$promise.then(function (data) {
                vm.publishers = data.publishers;
            })
            vm.categories = dataServiceFactory.getService('category').query({ isNested: false });
            vm.genres = dataServiceFactory.getService('genres').query();
            vm.subgenres = dataServiceFactory.getService('subgenres').query();
            vm.languages = dataServiceFactory.getService('languages').query();
        }

        vm.cancel = function () {
            vm.instance = vm.backup;
        }

        vm.save = function () {
            var book = {
                Id: vm.instance.Id,
                Title: vm.instance.Title,
                CategoryId: vm.instance.CategoryId,
                Authors: vm.instance.Authors,
                // Genres: vm.instance.genresNames,
                SubgenreId: vm.instance.SubgenreId,
                PublisherId: vm.instance.PublisherId,
                PublishYear: vm.instance.PublishYear,
                OriginalLangId: vm.instance.OriginalLangId,
                PublishLangId: vm.instance.PublishLangId,
                TotalPages: vm.instance.TotalPages,
                State: 1
            }
            dataServiceFactory.getService("Books").update(book);
        }

        vm.addAuthor = function () {
            var selectedAuthor;
            if (vm.authorId != undefined) {
                for (var i = 0; i < vm.allAuthors.length; i++) {
                    if (vm.allAuthors[i].Id == vm.authorId) {
                        selectedAuthor = vm.allAuthors[i];
                        selectedAuthor.State = 0;
                        vm.instance.Authors.push(selectedAuthor);
                        vm.allAuthors.splice(i, 1);
                    }
                }
            }
        }

        vm.deleteAuthor = function (author) {
            var selectedAuthor;
            if (author.Id != undefined) {
                for (var i = 0; i < vm.instance.Authors.length; i++) {
                    if (vm.instance.Authors[i].Id == author.Id) {
                        selectedAuthor = vm.instance.Authors[i];
                        selectedAuthor.State = 3;
                        vm.instance.Authors.splice(i, 1);
                        vm.allAuthors.push(selectedAuthor);
                        return;
                    }
                }
            }
        }

        vm.addGenre = function () {

        }

        vm.deleteGenre = function (genre) {

        }
    }
})();