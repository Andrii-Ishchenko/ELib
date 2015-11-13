(function () {
    angular.module("elib")
           .factory("CommentsRepository", CommentsRepository);

    CommentsRepository.$inject = ['$resource', 'COMMENT_CONST'];

    function CommentsRepository($resource, COMMENT_CONST) {
        var DataService = {
            getCommentsByBookId: getCommentsByBookId,
            postComment: postComment
        };

        return DataService;

        function getCommentsByBookId() {
            var url = COMMENT_CONST.COMMENT_URL;
            return $resource(url, { id: '@id' }, {
                get: {
                    query: 'Get',
                    isArray: true
                }
            });
        }

        function postComment(text) {
            var url = COMMENT_CONST.COMMENT_URL;
            return $resource(url, { id: '@id' }, {
                get: {
                    query: 'Get',
                    isArray: true
                }
            });
        }
    }
})();