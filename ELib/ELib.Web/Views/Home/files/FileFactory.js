(function () {
    angular.module("elib")
           .factory("fileFactory", fileFactory);

    fileFactory.$inject = ['$http'];

    function fileFactory($http) {
        var baseUrl = "/api/";

        var FileService =
            {
                uploadBookImage: uploadBookImage,
                uploadProfileImage: uploadProfileImage,
                uploadAuthorImage:uploadAuthorImage,
                downloadFile: downloadFile,
                uploadBookFile: uploadBookFile
            };

        return FileService;

        function uploadProfileImage(formData) {
             var url = baseUrl + "file/profile-image";

            return $http.post(url, formData, {
                withCredentials: true,

                headers: {
                    'Content-Type': undefined
                },
                transformRequest: angular.identity
            });
            
        }

        function uploadBookFile(formData, bookId) {
            var url = baseUrl + "file/book-instance/" + bookId;

            return $http.post(url, formData, {
                withCredentials: true,

                headers: {
                    'Content-Type': undefined
                },
                transformRequest: angular.identity
            });
        }


        function uploadBookImage(formData, bookId) {
            var url = baseUrl + "file/book-image/"+bookId;

            return $http.post(url, formData, {
                withCredentials: true,
                headers: {
                    'Content-Type': undefined
                },
                transformRequest: angular.identity
            });
        }

        function uploadAuthorImage(formData, authorId) {
            var url = baseUrl + "file/author-image/" + authorId;

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