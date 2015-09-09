(function () {
    angular.module("elib")
           .controller("NewBookController", NewBookController);

    NewBookController.$inject = ["authServiceFactory", "dataServiceFactory", "$location"];

    function NewBookController(authServiceFactory, dataServiceFactory,  $location) {
        var vm = this;

        authServiceFactory.fillAuthData();
        
        if (!authServiceFactory.authentication.isAuth) {
            $location.path('/login');
        }

        var languages = dataServiceFactory.getService('languages').query();

        vm.originalLanguages = languages;
        vm.publishLanguages = languages;

        //    vm.publishers = dataServiceFactory.getService('publishers').query();

        // need improvement
        var obj = dataServiceFactory.getService('publishers').get({ pageCount: 100, pageNumb: 1 });
        obj.$promise.then(function (data) {
            vm.publishers = data.publishers;
        })

        vm.subgenres = dataServiceFactory.getService('subgenres').query();
        
        vm.createBook = function () {
            var book = {
                Title: vm.title,
                PublishLangId: vm.publishLanguage,
                OriginalLangId: vm.originalLanguage,
                Isbn: vm.isbn,
                PublisherId: vm.publisher,
                Description: vm.description,
                SubgenreId: vm.subgenre
            }

            dataServiceFactory.getService('books').save(book).$promise.then(
                 //success
                 function (value) {
                     alert('success');
                 },
                 //error
                 function (error) {
                     alert('error');
                 }
            );
        }
    }
})();