(function () {
    'use strict';

    angular
        .module('elib')
        .directive('sortingDirective', sortingDirective);

    sortingDirective.$inject = ['COMMON_CONST'];

    function sortingDirective(COMMON_CONST) {
        return {
            restrict: 'E',
            scope: {
                ordering:"=",
                callback:"="
            },
            controller:function(){},
            link: function(scope,element,attrs){
             
              
                scope.changeOrder = function (item) {
                    if (scope.ordering.orderBy == item) {
                        if (scope.ordering.orderDirection == "ASC") {
                            scope.ordering.orderDirection = "DESC";
                        } else {
                            scope.ordering.orderDirection = "ASC";
                        }

                    } else {
                        scope.ordering.orderBy = item;
                        scope.ordering.orderDirection = "ASC";
                    }
                    //need for callback calling in new $digest loop after scope changed.
                    $timeout(scope.callback(), 0, true);     
                }

                //assign default values
                if (!scope.ordering.orderBy) {
                    scope.ordering.orderBy = scope.ordering.defaultOrder;                    
                }

                //assign default values
                if (!scope.ordering.orderDirection)
                {
                    scope.ordering.orderDirection = scope.ordering.defaultDirection;                 
                }

            },
           
            templateUrl: COMMON_CONST.SORTING
        }
    }

})();