﻿(function () {
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
                defaultDirection: "=",
                orderParameters:"="
            },
            controller:function(){},
            link: function(scope,element,attrs){
             
                scope.changeOrder = function (item, node) {
                    if (scope.orderBy == item) {
                        if (scope.orderDirection == "ASC") {
                            scope.orderDirection = "DESC";
                        } else {
                            scope.orderDirection = "ASC";
                        }

                    } else {
                        scope.orderBy = item;
                        scope.orderDirection = "ASC";                    
                    }

                   // var arrow = $("#sorting-direction-arrow", element).first().detach();
                    //arrow.append();
                }

               // arrow.insertAfter(selected);

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