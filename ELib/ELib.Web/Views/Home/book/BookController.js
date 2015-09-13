(function () {
    angular.module("elib")
           .controller("BookController", BookController);

    BookController.$inject = ["bookRepository", '$routeParams', "FileFactory", "$scope"];

    function BookController(bookRepository, $routeParams, FileFactory, $scope) {
        var vm = this;
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

        vm.downloadFile = function () {
            var downloadPath = "/api/file/book-download/78E43838F7B4D4A07D98DA9F699105F776CE71E6";
            window.open(downloadPath, '_blank', '');
        };

        $scope.uploadBookFile = function (file) {
            var fd = new FormData();
            fd.append("file", file[0]);

            FileFactory.uploadBookFile(fd, $routeParams.id).then(
                function (response) {
                    alert("success");
                },
                function (error) {
                    alert("error");
                });
        };
    }
})();