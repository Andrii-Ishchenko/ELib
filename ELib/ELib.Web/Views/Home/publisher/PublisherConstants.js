(function () {
    angular.module("elib")
           .constant('PUBLISH_CONST', (function () {
               return {
                   'PUBLISHER':   "/api/publishers/id/publisher",
                   'LOGIN':       '/login',
                   'PUBLISHERS':  '/publishers/',
                   'SUCCES':      "Publisher has been created successfully, you will be redicted to publisher page in 2 seconds.",
                   'ERROR':       "Publisher creation is failed"
               }
           })());
})();