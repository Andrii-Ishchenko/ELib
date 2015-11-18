(function () {
    'use strict';

    angular
        .module('elib')
        .directive('bookListDirective', bookListDirective);

    function bookListDirective($compile) {
        return {
            restrict: 'A',
            scope: {
                books: '='
            },
            templateUrl: '/views/home/book/book-list-item.html'
        }
    }
})();