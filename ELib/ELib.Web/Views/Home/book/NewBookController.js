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

        var languages = dataServiceFactory.getService('languages').query();

        vm.originalLanguages = languages;
        vm.publishLanguages = languages;

        //    vm.publishers = dataServiceFactory.getService('publishers').query();

        // need improvement
        var obj = dataServiceFactory.getService('publishers').get({ pageCount: 100, pageNumb: 1 });
        obj.$promise.then(function (data) {
            vm.publishers = data.publishers;
        })

        vm.categories = dataServiceFactory.getService('category').query();

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
                CategoryId: vm.category
            }

            dataServiceFactory.getService('books').save(book).$promise.then(
                 //success
                 function (value) {
                     vm.createdSuccessfully = true;
                     vm.message = "Book has been created successfully, you will be redicted to book page in 2 seconds.";
                     startTimer(value.BookId);
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