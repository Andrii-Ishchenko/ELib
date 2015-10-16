(function () {
    'use strict';

    angular
        .module('elib')
        .directive('sortingDirective', sortingDirective);

    function sortingDirective($timeout) {
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
                    $timeout(scope.callback(), 0, true);
                    //scope.$apply();
                    

                    
                }


                if (!scope.ordering.orderBy) {
                    scope.ordering.orderBy = scope.ordering.defaultOrder;
                    
                }

                if (!scope.ordering.orderDirection)
                {
                    scope.ordering.orderDirection = scope.ordering.defaultDirection;
                   
                }

                //scope.$watch('orderBy', function (newValue, oldValue) {
                //    scope.callback();
                //});

                //scope.$watch('orderDirection', function (newValue, oldValue) {
                //    scope.callback();
                //})

            },
           
            templateUrl: '/views/home/common/sorting-directive.html'
        }
    }

})();