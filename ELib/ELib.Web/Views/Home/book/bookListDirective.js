(function () {
    'use strict';

    angular
        .module('elib')
        .directive('bookListDirective', bookListDirective);

    bookListDirective.$inject = ['BOOK_CONST'];

    function bookListDirective($compile, BOOK_CONST) {
        return {
            restrict: 'A',
            scope: {
                books: '='
            },
            templateUrl: LIST_URL
        }
    }
})();