(function () {
    angular.module("elib")
           .controller("BookController", BookController);

    BookController.$inject = ["bookRepository", '$routeParams', "fileFactory"];

    function BookController(bookRepository, $routeParams, fileFactory) {
        var vm = this;

        vm.savedSuccessfully = false;
        vm.message = "";

        vm.instance = bookRepository.getBookById().get({ id: $routeParams.id });
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

        vm.uploadBookImage = function (file) {
            var fd = new FormData();
            fd.append("file", file[0]);

            fileFactory.uploadBookImage(fd,vm.instance.Id).then(
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
    }
})();