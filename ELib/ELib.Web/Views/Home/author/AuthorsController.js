(function () {
    angular.module("elib")
           .controller("AuthorsController", AuthorsController);

   AuthorsController.$inject = ["dataServiceFactory", "$scope"];

    function AuthorsController(dataServiceFactory, $scope) {
        var vm = this;

        $scope.template = {
            menu: "/views/shared/menu.html",
            main: "/views/home/author/authors.html"
        }

        vm.authors = dataServiceFactory.getService('authors').query();

        //activate();

        //function activate() {
        //    return getBooks().then(function () {
        //        console.log('Activated Books View');
        //    });
        //}

        //function getBooks() {
        //    return DataService.getAll('book')
        //    .then(function (data) {
        //        vm.books = data;
        //        console.log(vm.books);
        //        return vm.books;
        //    });
        //}
    }
})();