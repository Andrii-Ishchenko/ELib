(function () {
    angular.module("elib")
           .constant('USER_CONST', (function () {
               return {
                   'PROFILE':         "/profile",
                   'RATINGS':         "/profile/ratings",
                   'COMMENTS':        '/profile/comments',
                   'FAVORITES':       "/profile/favs",
                   'WISHLIST':        "/profile/books/wishlist",
                   'DONELIST':        "/profile/books/donelist",
                   'SOTIAL_NETWORKS': "/profile/social-networks",
                   'CURR_PROFILE':    "/api/CurrentProfile",
                   'GENERAL_INFO':    "/views/home/user/profile-general.html",
                   'USER':            "/api/Profile/GetCurrentUser"
               }
           })());
})();