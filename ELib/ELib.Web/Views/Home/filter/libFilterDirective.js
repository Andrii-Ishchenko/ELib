(function () {
    angular.module("elib")
           .directive("libFilter", libFilter);

    function libFilter() {
        var directiveObj = {
            restrict: 'E',
            scope: {
                entity: '@',
                type: '@',
                property: '@',
                sourceName: '@',
                label: '@',
                placeholder: '@'
            },
            templateUrl: getTemplate,
            controller: FilterController,
            controllerAs: 'filter',
            link: onFilter
        };

        FilterController.$inject = ["$scope", "$element", "$attrs", "$location", "$routeParams", "dataServiceFactory"];
        function FilterController($scope, $element, $attrs, $location, $routeParams, dataServiceFactory) {
            var vm = this;
            vm.entity = $scope.entity;
            vm.type = $scope.type;
            vm.property = $scope.property;
            vm.sourceName = $scope.sourceName;
            vm.currentYear = new Date().getFullYear();
            vm.label = $scope.label;
            vm.placeholder = $scope.placeholder;
            if (vm.type == "select") {
                vm.source = dataServiceFactory.getService(vm.sourceName).query();
            }
            vm.removeFilter = removeFilter;
            vm.addFilter = addFilter;

            function removeFilter(property) {
                vm["value"] = undefined;
                $location.search(property, undefined);
            }

            function addFilter() {
                var valid = true;
                if (vm.type == "text") {
                    valid = isValid(vm.value);
                }
                if (valid) {

                    if ($location.path() !== getFilterPathParameters().searchPath) {
                        $location.path(getFilterPathParameters().searchPath);
                    }
                    $location.search(getFilterPathParameters().parameters);
                    $location.search(vm.property, vm.value);
                }
            }

            function isValid(str) {
                return str !== "" && str !== " ";
            }

            function getFilterPathParameters() {
                var searchPath = {
                    "book": "/books/search",
                    "author": "/authors/search"
                };
                var parameters = {
                    "book": {
                        title: $routeParams.title,
                        author: $routeParams.author,
                        genre: $routeParams.genre,
                        genreId: $routeParams.genreId,
                        subgenreId: $routeParams.subgenreId,
                        subgenre: $routeParams.subgenre,
                        year: $routeParams.year,
                        categoryId : $routeParams.categoryId,
                        query: $routeParams.query,
                        pageCount: $routeParams.pageCount,
                        pageNumb: $routeParams.pageNumb
                    },
                    "author": {
                        authorName: $routeParams.authorName,
                        year: $routeParams.year,
                        query: $routeParams.query,
                        pageCount: $routeParams.pageCount,
                        pageNumb: $routeParams.pageNumb
                    }
                };
                return {
                    searchPath: searchPath[vm.entity],
                    parameters: parameters[vm.entity]
                };
            }
        }

        onFilter.$inject = ["$scope", "$element", "$attrs", "$ctrl"];
        function onFilter($scope, $element, $attrs, $ctrl) {
            $element.bind("keypress", onKeyPress);
            $element.bind("blur", function () { $scope.$apply($ctrl.addFilter); });
            if ($scope.type == "select") {
                $element.bind("change", function () { $scope.$apply($ctrl.addFilter); });
            }

            function onKeyPress(event) {
                var keyCode = event.which || event.keyCode;
                if (keyCode === 13 || keyCode === 9) {
                    $scope.$apply($ctrl.addFilter);
                    event.preventDefault();
                }
            }
        }

        getTemplate.$inject = ["$element", "$attrs"];
        function getTemplate($element, $attrs) {
            var templateUrls = {
                "text": "/views/home/filter/text-filter.html",
                "year": "/views/home/filter/year-filter.html",
                "select": "/views/home/filter/select-filter.html"
            };
            var type = $attrs["type"];
            return templateUrls[type];
        }

        return directiveObj;
    }
})();