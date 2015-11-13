(function () {
    'use strict';

    angular
        .module('elib')
        .directive('commentsDirective', commentsDirective);

    commentsDirective.$inject = ['COMMENT_CONST'];

    function commentsDirective($timeout, COMMENT_CONST) {
        return {
            restrict: 'E',
            scope: {
                comments: "=",
                fetch: "=",
                newComment: "=",
                cleanComment: "=",
                createComment: "=",
                canPost: "=",
                profile: "=",
                canLoad: "="

            },
            controller: function () { },
            link: function (scope, element, attrs) { },

            templateUrl: COMMENT_CONST.COMMENTS_URL
        }
    }
})();