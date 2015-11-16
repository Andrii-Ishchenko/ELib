(function () {
    'use strict';

    angular
        .module('elib')
        .directive('bookListDirective', bookListDirective);

   // bookListDirective.$inject = ['BOOK_CONST'];

    function bookListDirective($compile, BOOK_CONST) {
        return {
            restrict: 'A',
            scope: {
                books: '='
            },
            templateUrl: '/views/home/book/book-list-item.html'
           // templateUrl: BOOK_CONST.LIST_URL
        }
    }
})();