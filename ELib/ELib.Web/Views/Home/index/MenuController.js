(function () {
    angular.module("elib")
           .controller("MenuController", MenuController);

    MenuController.$inject = ["$location", "$route", "$scope", "dataServiceFactory", 'INDEX_CONST'];

    function MenuController($location, $route, $scope, dataServiceFactory, INDEX_CONST) {
        var urls = {
            books: INDEX_CONST.BOOKS_LIST,
            authors: INDEX_CONST.AUTHORS,
            publishers: INDEX_CONST.PUBLISHERS,
            profile: INDEX_CONST.PROFILE
        };

        var vm = this;

        vm.statusBooks = {
            isFirstOpen: true,
            isSecondOpen: true
        };

        vm.statusAuthors = {
            isFirstOpen: true
        };

        vm.ToggleNode = function ToggleNode(node) {
            if (node && node.opened != undefined)
                node.opened = !node.opened
        }

        var catParameters = {
            isNested: true
        }

        var obj = dataServiceFactory.getService('category').query(catParameters)
            .$promise.then(function (data) {
                vm.categories = data;
                preProcessCategories(vm.categories, 0);
            });


        changeMenu();
        $scope.$on("$locationChangeStart", changeMenu);
        function changeMenu() {
            switch ($location.path()) {
                case INDEX_CONST.BOOKS:;
                case INDEX_CONST.BOOK_SEARCH: vm.Url = urls["books"];
                    break;
                case INDEX_CONST.AUTHORS:;
                case INDEX_CONST.AUTHOR_SEARCH: vm.Url = urls["authors"];
                    break;
                case INDEX_CONST.PUBLISHERS: vm.Url = urls["publishers"];
                    break;
                case INDEX_CONST.PROF:;
                case INDEX_CONST.PROF_RATINGS :;
                case INDEX_CONST.PROF_COMMENTS:;
                case INDEX_CONST.PROF_FAVS:;
                case INDEX_CONST.WISHLIST:;
                case INDEX_CONST.DONELIST:;
                case INDEX_CONST.SOTIAL_NETWORK: vm.Url = urls["profile"];
                    break;
                default: vm.Url = "";
                    break;
            }
        }

        function preProcessCategories(children, level) {

            for (var index in children) {
                if (children[index].Level != undefined && children[index].Level >= level) {
                    children[index].opened = false;
                } else {
                    children[index].opened = true;
                }
                preProcessCategories(children[index].Children, level)
            }
        }
    }
})();