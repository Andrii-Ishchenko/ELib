(function () {
    angular.module("elib")
           .constant('STATE', {
               'AUTHOR': "/api/authors/id/author",
               'MAX_SIZE': '5',
               'PAGE_COUNT': '5',
               'FIRST_PAGE': '1',
               'LOGIN': '/login',
               'AUTHORS': '/authors/',
               'CREATION_SUCCESFUL':"Author has been created successfully, you will be redicted to author page in 2 seconds.",
               'CREATION_FAILED':"Author creation is failed"
           });    
})();