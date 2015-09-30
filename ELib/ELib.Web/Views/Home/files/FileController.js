(function () {
    angular.module("elib")
           .controller("FileController", FileController);

    FileController.$inject = ["fileFactory"];

    function FileController(fileFactory) {
        var vm = this;

        vm.uploadProfileImage = function (file) {
            var fd = new FormData();
            fd.append("file", file[0]);

            FileFactory.uploadProfileImage(fd);

        }
    }

})();