//(function () {
//    'use strict';

//    angular
//        .module('elib')
//        .directive('commentsDirective', commentsDirective);

//    function commentsDirective($compile) {
//        return {
//            restrict: 'E',
//            scope: {
//                comments: '='
//            },
//            templateUrl: '/Views/Home/comments/Comments.html'
//        }
//    }

//})();


(function () {
    'use strict';

    angular
        .module('elib')
        .directive('commentsDirective', commentsDirective);

    function commentsDirective($timeout) {
        return {
            restrict: 'E',
            scope: {
                comments: "=",
                fetch: "=",
                newComment: "=",
                cleanComment: "=",
                createComment: "=",
                canPost:"=",
                profile:"=",
                canLoad:"="

            },
            controller: function () { },
            link: function (scope, element, attrs) {
                //var raw = element[0];

                //element.bind('scroll', function () {
                //    if (raw.scrollTop + raw.offsetHeight >= raw.scrollHeight) {
                //        scope.$apply(attr.whenScrolled);
                //    }
                //});
            },

            templateUrl: '/views/home/comments/Comments.html'
        }
    }

})();