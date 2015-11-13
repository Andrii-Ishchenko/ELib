(function () {
    angular.module("elib")
           .constant('AUTHOR_CONST', {
               'MAX_SIZE':           5,
               'PAGE_COUNT':         5,
               'FIRST_PAGE':         1,
               'LOGIN':              '/login',
               'AUTHORS':            '/authors/',
               'AUTHOR':             "/api/authors/id/author",
               'CREATION_FAILED':    "Author creation is failed",
               'CREATION_SUCCESFUL': "Author has been created successfully, you will be redicted to author page in 2 seconds."
           });    
})();