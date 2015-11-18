(function () {
    angular.module("elib")
           .controller("PublishersController", PublishersController);

    PublishersController.$inject = ["dataServiceFactory", '$routeParams', '$location'];

    function PublishersController(dataServiceFactory, $routeParams, $location) {
        var vm = this;

        vm.pageCount = ($routeParams.pageCount) ? $routeParams.pageCount : "5";
        vm.currPage = ($routeParams.pageNumb) ? $routeParams.pageNumb : 1;
        vm.maxSize = 5;
        vm.ordering = {
            orderBy: 'Name',
            orderDirection: ($routeParams.orderDirection) ? $routeParams.orderDirection : 'DESC',
            defaultOrder: "Name",
            defaultDirection: "DESC",
            orderParameters: ["Name"]
        };

        vm.pageChanged = pageChanged;
        vm.OrderingChanged = function () {
            getPublishers();
        }

        getPublishers();
        
        //gets publishers list for query with parameters; receives the total count of publisher from response headers.
        function getPublishers() {
            var publishParam = {
                pageCount: vm.pageCount,
                pageNumb: vm.currPage,
                query: $routeParams.query,
                orderBy: vm.ordering.orderBy,
                orderDirection: vm.ordering.orderDirection
            }
            vm.publishers = dataServiceFactory.getService('publishers').query(publishParam, onSuccess = function (response, headers) {
                vm.totalCount = headers("totalCount");
            },
                                                                                            onError = function (response) { });
        }

        function pageChanged() {
            $location.search("pageNumb", vm.currPage);
            $location.search("pageCount", vm.pageCount);
            $location.search("orderBy", vm.ordering.orderBy);
            $location.search("orderDirection", vm.ordering.orderDirection);
        }
    }
})();