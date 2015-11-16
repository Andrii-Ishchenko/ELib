(function () {
    angular.module("elib")
           .constant('REGISTR_CONST', (function () {
               return {
                   'SAVE_SUCCESSFULLY':  "User has been registered successfully, you will be redicted to login page in 2 seconds.",
                   'SAVE_FAILED':        "Registration is failed",
                   'ERROR':              "Failed to register user due to:",
                   'LOGIN':              '/login'
               }
           })());
})();