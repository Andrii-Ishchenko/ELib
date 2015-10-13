(function () {
    'use strict';

    angular
        .module('elib')
        .directive('sortingDirective', sortingDirective);

    function sortingDirective($compile) {
        return {
            restrict: 'E',
            scope: {
                orderBy: "=",
                orderDirection: "=",
                defaultOrder: "=",
                defaultDirection: "="
            },
            link: function(scope,element,attrs){
                scope.items = attrs.params.split(",");

                var selected = $(".bg-primary", element)[0];
                var arrow = $("#sorting-direction-arrow", element)[0];
                arrow.insertAfter(selected);

                if (!scope.orderBy) {
                    scope.orderBy = scope.defaultOrder;
                }

                if(!scope.orderDirection)
                {
                    scope.orderDirection = scope.defaultDirection;
                }
            },
           
            templateUrl: '/views/home/common/sorting-directive.html'
        }
    }

})();