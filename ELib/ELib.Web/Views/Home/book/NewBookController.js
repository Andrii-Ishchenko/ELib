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
        vm.author = {};
        vm.authorsMas = [];
        vm.selectedAuthorIndex = "";

        var languages = dataServiceFactory.getService('languages').query();

        vm.originalLanguages = languages;
        vm.publishLanguages = languages;

        // need improvement
        var obj = dataServiceFactory.getService('publishers').get({ pageCount: 100, pageNumb: 1 });
        obj.$promise.then(function (data) {
            vm.publishers = data.publishers;
        })
        vm.authors = dataServiceFactory.getService('authors').get(null).$promise.then(function (data) {
            vm.authors = data.authors;
        })

        vm.authorSelected = function () {
            if (vm.author != undefined) {
                var selectedAuthor = vm.authors[vm.authorId - 1];
                vm.authorsMas.push(selectedAuthor);
                var index = vm.authors.indexOf(selectedAuthor);
                vm.authors.splice(index, 1);
            }           
        }

        vm.deleteAuthor = function (author2) {
           /* console.log("1111");
            if (author != undefined) {
                console.log("22222");
                vm.authorsMas.pop(author);
                console.log(vm.authorsMas);
            }   */         
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
                PublishYear: vm.yearOfPublishing
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