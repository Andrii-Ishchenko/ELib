(function () {
    angular.module("elib")
           .controller("BookController", BookController);

    BookController.$inject = ['$routeParams', "fileFactory", "$scope", "authServiceFactory", "dataServiceFactory", 'BOOK_CONST'];

    function BookController($routeParams, fileFactory, $scope, authServiceFactory, dataServiceFactory, BOOK_CONST) {
        
        vm = this;
        vm.isEditMode = false;
        vm.savedSuccessfully = false;
        vm.message = "";

        //GET api/CurrentProfile
        vm.profile = dataServiceFactory.getService("CurrentProfile").get();

        //GET api/books/id
        vm.instance = dataServiceFactory.getService("books").get({ id: $routeParams.id });
 
        vm.changeRating = function () {
            createRating();
        };

        var createRating = function () {
            vm.submitState = true;
            var rating = {
                ValueRating: vm.instance.Rating,
                UserId: vm.profile.Id,
                BookId: parseInt($routeParams.id),
                State : 1
            }
            dataServiceFactory.getService('Ratings').save(rating).$promise.then(
              //success
              function (value) {
                  //GET api/books/id
                  vm.instance = dataServiceFactory.getService("books").get({ id: $routeParams.id});
              }
         );
        };

        

        vm.uploadBookImage = function (file) {
            var fd = new FormData();
            fd.append("file", file[0]);

            fileFactory.uploadBookImage(fd, vm.instance.Id).then(
                function (response) {
                    vm.instance = dataServiceFactory.getService("books").get({ id: $routeParams.id});
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
                    var bookInstance = dataServiceFactory.getService("books").get({ id: $routeParams.id})
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

            dataServiceFactory.getService('authors').query().$promise.then(function (data) {
                vm.allAuthors = data;
                for (var a = 0; a < vm.instance.Authors.length; a++) {
                    for (var i = 0; i < vm.allAuthors.length; i++) {
                        if (vm.instance.Authors[a].Id == vm.allAuthors[i].Id) {
                            vm.allAuthors.splice(i, 1);
                            break;
                        }
                    }
                }
            })
            vm.publishers = dataServiceFactory.getService('publishers').query();
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