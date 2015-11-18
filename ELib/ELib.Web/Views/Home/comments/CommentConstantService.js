(function () {
    angular.module("elib")
           .constant('COMMENT_CONST', {
               'COMMENT_URL': "/api/Comments/:id/comments-for-book",
               'COMMENTS_URL': "/views/home/comments/Comments.html"
           });
})();