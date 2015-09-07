(function () {
    angular.module("elib")
           .factory("authInterceptorServiceFactory", authInterceptorServiceFactory);

    authInterceptorServiceFactory.$inject = ['$q', '$location', 'localStorageService'];

    function authInterceptorServiceFactory($q, $location, localStorageService) {

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
            if (rejection.status === 401) {
                $location.path('/login');
            }
            return $q.reject(rejection);
        }
    }
})();