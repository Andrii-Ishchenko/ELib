(function () {
    angular.module("elib")
           .controller("BookController", BookController);

    BookController.$inject = ["dataServiceFactory", '$routeParams'];

    function BookController(dataServiceFactory, $routeParams) {
        var vm = this;
        vm.instance = dataServiceFactory.getService('books').get({ id: $routeParams.id });
        vm.getFullStarsArray = function () {
            var fullStarsNumb = parseInt(vm.instance.Rating);
            var arr = [];
            if (fullStarsNumb > 0) arr.length = fullStarsNumb;
            return arr;
        };

        vm.getEmptyStarsArray = function () {
            var EmptyStarsNumb = 10 - parseInt(vm.instance.Rating);
            var arr = [];
            if (EmptyStarsNumb > 0) arr.length = EmptyStarsNumb;
            return arr;
        };
    }
})();