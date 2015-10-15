angular.module("elib")
       .controller("MenuController", MenuController);

MenuController.$inject = ["$location", "$route", "$scope"];

function MenuController($location, $route, $scope) {
    var urls = {
        books: "views/home/book/book-list-menu.html",
        authors: "views/home/author/authors-menu.html",
        publishers: "views/home/publisher/publishers-menu.html",
        profile: "views/home/user/profile-menu-current.html"
    };

    var vm = this;
    changeMenu();
    $scope.$on("$locationChangeStart", changeMenu);
    function changeMenu() {
        console.log($location.path());
        switch ($location.path()) {
            case "/books" :;
            case "/books/search" : vm.Url = urls["books"];
                break;
            case "/authors":;
            case "/authors/search" : vm.Url = urls["authors"];
                break;
            case "/publishers": vm.Url = urls["publishers"];
                break;
            case "/profile":;
            case "/profile/ratings":;
            case "/profile/comments":;
            case "/profile/favs":;
            case "/profile/books/wishlist":;
            case "/profile/books/donelist":;
            case "/profile/social-networks" : vm.Url = urls["profile"];
                break;
            default: vm.Url = "";
                break;
        }
    }
}
