﻿(function () {
    'use strict';

    angular
        .module('elib')
        .directive('commentsDirective', commentsDirective);

    function commentsDirective($compile) {
        return {
            restrict: 'A',
            scope: {
                comments: '='
            },
            templateUrl: '/Views/Home/comments/Comment.html'
        }
    }

})();