(function () {
    angular.module("elib")
           .constant('FILTER_CONST', (function () {
               return {
                   'BOOK_SEARCH':    "/books/search",
                   'AUTHOR_SEARCH':  "/authors/search",
                   'TEXT_FILTER':    "/views/home/filter/text-filter.html",
                   'YEAR_FILTER':    "/views/home/filter/year-filter.html",
                   'SELECT_FILTER':  "/views/home/filter/select-filter.html"
               }
           })());
})();