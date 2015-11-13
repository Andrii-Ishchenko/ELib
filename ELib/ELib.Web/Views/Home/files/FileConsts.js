(function () {
    angular.module("elib")
           .constant('FILE_CONST', {
               'PROFILE_IMAGE_URL':  "/api/file/profile-image",
               'BOOK_INSTANCE_URL':  "/api/file/book-instance/",
               'BOOK_IMG_URL':       "/api/file/book-image/",
               'AUTHOR_IMG_URL':     "/api/file/author-image/"
           });
})();