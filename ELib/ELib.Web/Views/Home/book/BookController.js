(function () {
    angular.module("elib")
           .controller("BookController", BookController);

    BookController.$inject = ["bookRepository", "CommentsRepository", '$routeParams', "fileFactory", "$scope", "authServiceFactory", "dataServiceFactory", "currentProfileFactory"];

    function BookController(bookRepository, CommentsRepository, $routeParams, fileFactory, $scope, authServiceFactory, dataServiceFactory, currentProfileFactory) {
        var vm = this;
        vm.isEditMode = false;
        vm.IsPostEnabled = true;
        vm.CanPost = function () {
            if (vm.IsPostEnabled && vm.newComment.Text.length > 0 && vm.newComment.Text.length < 400)
                return true;
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

        vm.ToggleEditMode = function () {
            vm.isEditMode = !vm.isEditMode;
        }

        vm.edit = function () {
            vm.backup = angular.copy(vm.instance);

            dataServiceFactory.getService('authors').get(null).$promise.then(function (data) {
                vm.allAuthors = data.authors;
                for (var a in vm.instance.Authors ) {
                    var index = vm.allAuthors.indexOf({ Id: a.Id });
                     vm.allAuthors.splice(index, 1);
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
                TotalPages: vm.instance.TotalPages
            }
            alert(book.Id + book.Title + book.CategoryId + book.SubgenreId + book.PublisherId + book.PublishYear + book.OriginalLangId + book.PublishLangId + book.TotalPages);
            dataServiceFactory.getService("Book").update(book);
        }

        vm.addAuthor = function () {
            var selectedAuthor;
            var index;
            if (vm.authorId != undefined) {
                for (var i = 0; i < vm.allAuthors.length; i++) {
                    if (vm.allAuthors[i].Id == vm.authorId) {
                        selectedAuthor = vm.allAuthors[i];
                        index = i;
                    }
                }
                vm.instance.Authors.push(selectedAuthor);
                vm.allAuthors.splice(index, 1);
            }
        }

        vm.deleteAuthor = function (author) {
            var selectedAuthor;
            var index;
            if (author.Id != undefined) {
                for (var i = 0; i < vm.instance.Authors.length; i++) {
                    if (vm.instance.Authors[i].Id == author.Id) {
                        selectedAuthor = vm.instance.Authors[i];
                        index = i;
                    }
                }
                vm.instance.Authors.splice(index, 1);
                vm.allAuthors.push(selectedAuthor);
            }
        }

        vm.addGenre = function () {

        }

        vm.deleteGenre = function (genre) {

        }
    }
})();