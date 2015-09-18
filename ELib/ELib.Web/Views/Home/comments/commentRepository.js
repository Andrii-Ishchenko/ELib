(function () {
    angular.module("elib")
           .factory("commentRepository", commentRepository);

    commentRepository.$inject = ['$resource'];

    function commentRepository($resource) {
        var baseUrl = "/api/Comments/";
        var DataService = {
            getCommentsByBookId: getCommentsByBookId
        };

        return DataService;

        function getCommentsByBookId() {
            var url = baseUrl + ":id/get";
            return $resource(url, { id: '@id' }, {
                get: {
                    query: 'Get',
                    isArray: true
                }
            });
        }
    }

})();