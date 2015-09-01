(function () {
    'use strict';

    angular
        .module('elib')
        .directive('authorListDirective', authorListDirective);

    function authorListDirective($compile) {
        return {
            restrict: 'A',
            scope: {
                authors: '='
            },
            templateUrl: '/views/home/author/author-list-item.html'
        }
    }

})();