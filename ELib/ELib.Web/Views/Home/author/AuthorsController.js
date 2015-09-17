(function () {
    angular.module("elib")
           .controller("AuthorsController", AuthorsController);

   AuthorsController.$inject = ["dataServiceFactory", "$scope", "$routeParams"];

    function AuthorsController(dataServiceFactory, $scope, $routeParams) {
        var vm = this;

        $scope.template = {
            menu: "/views/shared/menu.html",
            main: "/views/home/author/authors.html"
        }

        vm.authors = dataServiceFactory.getService('authors').query({ query: $routeParams.query });

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