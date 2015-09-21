(function () {
    angular.module("elib")
           .factory("CommentsRepository", CommentsRepository);

    CommentsRepository.$inject = ['$resource'];

    function CommentsRepository($resource) {
        var baseUrl = "/api/Comments/";
        var DataService = {
            getCommentsByBookId: getCommentsByBookId,
            postComment: postComment
        };

        return DataService;

        function getCommentsByBookId() {
            var url = baseUrl + ":id/comments-for-book";
            return $resource(url, { id: '@id' }, {
                get: {
                    query: 'Get',
                    isArray: true
                }
            });
        }

        function postComment(text) {
            var url = baseUrl + ":id/comments-for-book";
            return $resource(url, { id: '@id' }, {
                get: {
                    query: 'Get',
                    isArray: true
                }
            });
        }
    }

})();