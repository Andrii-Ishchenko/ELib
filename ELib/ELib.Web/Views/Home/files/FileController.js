(function () {
    angular.module("elib")
           .controller("FileController", FileController);

    FileController.$inject = ["FileFactory", "$scope"];

    function FileController(FileFactory, $scope) {
        var vm = this;

        $scope.uploadProfileImage = function (file) {
            var fd = new FormData();
            fd.append("file", file[0]);

            FileFactory.uploadProfileImage(fd);

        }
    }

})();