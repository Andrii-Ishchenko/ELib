(function () {
    angular.module("elib")
           .controller("NewBookController", NewBookController);

    NewBookController.$inject = ["authServiceFactory", "dataServiceFactory", "$location", "$timeout"];

    function NewBookController(authServiceFactory, dataServiceFactory, $location, $timeout) {
        var vm = this;

       authServiceFactory.fillAuthData();
        
        if (!authServiceFactory.authentication.isAuth) {
            $location.path('/login');
        }

        vm.message = '';
        vm.submitState = false;
        vm.createdSuccessfully = false;
        vm.currentYear = new Date().getFullYear();
        vm.authorsMas = [];

        var languages = dataServiceFactory.getService('languages').query();

        vm.originalLanguages = languages;
        vm.publishLanguages = languages;

        // need improvement
        var obj = dataServiceFactory.getService('publishers').get(null).$promise.then(function (data) {
            vm.publishers = data.publishers;
        });
        vm.authors = dataServiceFactory.getService('authors').get(null).$promise.then(function (data) {
            vm.authors = data.authors;
        });

        vm.authorSelected = function () {
            var selectedAuthor;
            var index;
            if (vm.authorId != undefined) {
                for (var i = 0; i < vm.authors.length; i++) {
                    if (vm.authors[i].Id == vm.authorId) {
                        selectedAuthor = vm.authors[i];
                        selectedAuthor.State = 0;
                        index = i;
                    }
                }
                vm.authorsMas.push(selectedAuthor);
                vm.authors.splice(index, 1);
            }           
        }

        vm.deleteAuthor = function (author) {
            var selectedAuthor;
            var index;
            if (author.Id != undefined) {
                for (var i = 0; i < vm.authorsMas.length; i++) {
                    if (vm.authorsMas[i].Id == author.Id) {
                        selectedAuthor = vm.authorsMas[i];
                        selectedAuthor.State = 2;
                        index = i;
                    }
                }
                vm.authorsMas.splice(index, 1);
                vm.authors.push(selectedAuthor);
            }      
        }
                
        var catParameters = {
            isNested: false
        }

        vm.categories = dataServiceFactory.getService('category').query(catParameters);

        vm.subgenres = dataServiceFactory.getService('subgenres').query();
        
        vm.createBook = function () {
            vm.submitState = true;

            var book = {
                Title: vm.title,
                PublishLangId: vm.publishLanguage,
                OriginalLangId: vm.originalLanguage,
                Isbn: vm.isbn,
                PublisherId: vm.publisher,
                Description: vm.description,
                SubgenreId: vm.subgenre,
                CategoryId: vm.category,
                PublishYear: vm.yearOfPublishing,
                Authors: vm.authorsMas,
                State : 0
            }

            dataServiceFactory.getService('books').save(book).$promise.then(
                 //success
                 function (value) {
                     vm.createdSuccessfully = true;
                     vm.message = "Book has been created successfully, you will be redicted to book page in 2 seconds.";
                     startTimer(value.Id);
                 },
                 //error
                 function (error) {
                     vm.submitState = false;
                     vm.message = "Book creation is failed";
                 }
            );
        };

        var startTimer = function (bookId) {
            var timer = $timeout(function () {
                $timeout.cancel(timer);
                $location.path('/books/' + bookId);
            }, 2000);
        };
    }
})();