(function () {
    angular.module("elib")
           .factory("fileFactory", fileFactory);

    fileFactory.$inject = ['$http', 'FILE_CONST'];

    function fileFactory($http, FILE_CONST) {

        var FileService =
            {
                uploadBookImage: uploadBookImage,
                uploadProfileImage: uploadProfileImage,
                uploadAuthorImage: uploadAuthorImage,
                downloadFile: downloadFile,
                uploadBookFile: uploadBookFile
            };

        return FileService;

        function uploadProfileImage(formData) {
            var url = FILE_CONST.PROFILE_IMAGE_URL;

            return $http.post(url, formData, {
                withCredentials: true,

                headers: {
                    'Content-Type': undefined
                },
                transformRequest: angular.identity
            });

        }

        function uploadBookFile(formData, bookId) {
            var url = FILE_CONST.BOOK_INSTANCE_URL + bookId;

            return $http.post(url, formData, {
                withCredentials: true,

                headers: {
                    'Content-Type': undefined
                },
                transformRequest: angular.identity
            });
        }

        function uploadBookImage(formData, bookId) {
            var url = FILE_CONST.BOOK_IMG_URL + bookId;

            return $http.post(url, formData, {
                withCredentials: true,
                headers: {
                    'Content-Type': undefined
                },
                transformRequest: angular.identity
            });
        }

        function uploadAuthorImage(formData, authorId) {
            var url = FILE_CONST.AUTHOR_IMG_URL + authorId;

            return $http.post(url, formData, {
                withCredentials: true,
                headers: {
                    'Content-Type': undefined
                },
                transformRequest: angular.identity
            });
        }

        function downloadFile() {
            console.log("download file function called.")
        }
    }
})();