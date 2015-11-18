(function () {
    angular.module("elib")
           .constant('LOGIN_CONST', (function () {
               return {
                   'BOOKS': '/books',
                   'ERROR': 'Login is failed'
               }
           })());
})();