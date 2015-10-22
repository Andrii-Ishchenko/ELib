(function () {
    angular.module("elib")
            .directive("libPageCountChanger", libPageCountChanger);

    function libPageCountChanger() {
        var directiveObj = {
            restrict: 'E',
            replace: true,
            scope : {
                entityName: '@'
            },
            templateUrl: "/views/home/common/lib-page-changer.html",
            controller: LibPageChangerController,
            controllerAs: 'pageChangeCtrl'
        };

        LibPageChangerController.$inject = ["$scope","$location", "$routeParams"];

        function LibPageChangerController($scope,$location, $routeParams) {
            vm = this;
            vm.entityName = $scope.entityName;
            vm.pageCount = ($routeParams.pageCount) ? $routeParams.pageCount : "5";
            vm.pageCountChanged = pageCountChanged;

            function pageCountChanged() {
                $location.search('pageCount', vm.pageCount);
            }
        }
        return directiveObj;
    }
})();