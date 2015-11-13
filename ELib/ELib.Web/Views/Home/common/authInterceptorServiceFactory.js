(function () {
    angular.module("elib")
           .factory("authInterceptorServiceFactory", authInterceptorServiceFactory);

    authInterceptorServiceFactory.$inject = ['$q', '$location', 'localStorageService', 'COMMON_CONST'];

    function authInterceptorServiceFactory($q, $location, localStorageService, COMMON_CONST) {

        var authInterceptorFactory = {
            request: request,
            responseError: responseError
        };
 
        return authInterceptorFactory;

        function request(config) {
            
            config.headers = config.headers || {};
 
            var authData = localStorageService.get('authorizationData');
            if (authData) {
                config.headers.Authorization = 'Bearer ' + authData.token;
            }
 
            return config;
        }
 
        function responseError (rejection) {
            if (rejection.status === COMMON_CONST.UNAUTHORIZED_USER) {
                $location.path(COMMON_CONST.LOGIN);
            }
            return $q.reject(rejection);
        }
    }
})();