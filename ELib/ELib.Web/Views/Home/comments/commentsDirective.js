(function () {
    'use strict';

    angular
        .module('elib')
        .directive('commentsDirective', commentsDirective);

    function commentsDirective($timeout){
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
            templateUrl: "/views/home/comments/Comments.html"
        }
    }
})();