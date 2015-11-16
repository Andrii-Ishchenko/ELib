(function () {
    angular.module("elib")
           .constant('INDEX_CONST', (function () {
               return {
                   'TYPE_BOOKS':        'books',
                   'TYPE_PUBLISHERS':   "publishers",
                   'TYPE_AUTHORS':      "authors",
                   'BOOK_SEARCH':       '/books/search',
                   'AUTHOR_SEARCH':     '/authors/search',
                   'PUBLISHER_SEARCH':  '/publishers/search',
                   'BOOKS_LIST':        "views/home/book/book-list-menu.html",
                   'AUTHORS':           "views/home/author/authors-menu.html",
                   'PUBLISHERS':        "views/home/publisher/publishers-menu.html",
                   'PROFILE':           "views/home/user/profile-menu-current.html",
                   'BOOKS':             "/books",
                   'AUTHORS':           "/authors",
                   'PROF':              "/profile",
                   'PROF_RATINGS':      "/profile/ratings",
                   'PROF_COMMENTS':     "/profile/comments",
                   'PROF_FAVS':         "/profile/favs",
                   'WISHLIST':          "/profile/books/wishlist",
                   'DONELIST':          "/profile/books/donelist",
                   'SOTIAL_NETWORK':    "/profile/social-networks"
               }
           })());
})();