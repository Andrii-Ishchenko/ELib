(function () {
    angular.module("elib")
           .controller("AuthorsController", AuthorsController);

    AuthorsController.$inject = ["dataServiceFactory", '$routeParams', "$location", 'AUTHOR_CONST'];

    function AuthorsController(dataServiceFactory, $routeParams, $location, AUTHOR_CONST) {
        var vm = this;
        vm.pageCount = ($routeParams.pageCount) ? $routeParams.pageCount : AUTHOR_CONST.PAGE_COUNT;
        vm.currPage = ($routeParams.pageNumb) ? $routeParams.pageNumb : AUTHOR_CONST.FIRST_PAGE;
        vm.maxSize = AUTHOR_CONST.MAX_SIZE;
        vm.ordering = fetchOrderingWithDefaultParams();

        vm.pageChanged = pageChanged;
        vm.OrderingChanged = function () {
            var params = getParameters();
            vm.authors = dataServiceFactory.getService('authors').query(params, onSuccess = function (response, headers) {
                                                                                    vm.totalItems = headers("totalCount");
                                                                                   },
                                                                                onError = function (response) {
                                                                                    console.log(response.status);
                                                                                });
                            
        }

        function fetchOrderingWithDefaultParams() {
            return {
                defaultOrder: "LastName",
                defaultDirection: "DESC",
                orderBy: ($routeParams.orderBy) ? $routeParams.orderBy : "LastName",
                orderDirection: ($routeParams.orderDirection) ? $routeParams.orderDirection : "DESC",
                orderParameters: ["FirstName", "LastName", "DateOfBirth"]
            }
        }

        var parameters = getParameters();
        vm.authors = dataServiceFactory.getService('authors').query(parameters, onSuccess = function (response, headers) {
                                                                                     vm.totalItems = headers("totalCount");
                                                                                 },
                                                                                onError = function (response) {
                                                                                    console.log(response.status);
                                                                                });

        function getParameters() {
            return {
                pageCount: vm.pageCount,
                pageNumb: vm.currPage,
                query: $routeParams.query,
                authorName: $routeParams.authorName,
                year: $routeParams.year,
                orderBy: vm.ordering.orderBy,
                orderDirection: vm.ordering.orderDirection
            };
        }

        function pageChanged() {
            $location.search(getParameters());
        }
    }
})();
