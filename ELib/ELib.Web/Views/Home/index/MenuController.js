(function () {
    angular.module("elib")
           .controller("MenuController", MenuController);

    MenuController.$inject = ["$location", "$route", "$scope", "dataServiceFactory"];

    function MenuController($location, $route, $scope, dataServiceFactory) {
        var urls = {
            books: "views/home/book/book-list-menu.html",
            authors: "views/home/author/authors-menu.html",
            publishers: "views/home/publisher/publishers-menu.html",
            profile: "views/home/user/profile-menu-current.html"
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
                case "/books":;
                case "/books/search": vm.Url = urls["books"];
                    break;
                case "/authors":;
                case "/authors/search": vm.Url = urls["authors"];
                    break;
                case "/publishers": vm.Url = urls["publishers"];
                    break;
                case "/profile":;
                case "/profile/ratings":;
                case "/profile/comments":;
                case "/profile/favs":;
                case "/profile/books/wishlist":;
                case "/profile/books/donelist":;
                case "/profile/social-networks": vm.Url = urls["profile"];
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