(function () {
    angular.module("elib")
           .constant('BOOK_CONST',(function() {               
               return {
                   'STARS_COUNT':         5,
                   'BASED_ON':            100,
                   'TIMEOUT':             2000,
                   'PAGE_COUNT':          5,
                   'CURRENT_PAGE':        1,
                   'MAX_TEXT':            400,
                   'LENGTH':              5,
                   'COUNT_COMMENTS':      5,
                   'CURRENT_COMMENTS':    1,
                   'MAX_SIZE':            5,
                   'BOOKS':               '/books/',
                   'LOGIN':               '/login',
                   'BOOK':                "book/:id",
                   'API_BOOKS':           "/api/books/",
                   'SEARCH':              "/books/search",
                   'RATING_CLASS':        'fa fa-star',
                   'AUTHOR_BOOK':         "/api/authors/id/books",
                   'PUBLISHER_BOOK':      "/api/publishers/id/books",
                   'CREATION_FAILED':     "Book creation is failed",
                   'LIST_URL':            '/views/home/book/book-list-item.html',
                   'UPLOADING_FAILED':    "Book file uploading is failed",
                   'UPLOADING_SUCCESFUL': "Book file has been uploaded successfully",
                   'CREATION_SUCCESFUL':  "Book has been created successfully, you will be redicted to book page in 2 seconds."
               }
           })());
})();