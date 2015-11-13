(function () {
    angular.module("elib")
           .factory("authServiceFactory", authServiceFactory);

    authServiceFactory.$inject = ['$http', '$q', 'localStorageService', 'COMMON_CONST'];

    function authServiceFactory($http, $q, localStorageService, COMMON_CONST) {
    
        var authentication = {
            isAuth: false,
            userName: ""
        };

        var AuthServiceFactory = {
            saveRegistration : saveRegistration,
            login : login,
            logOut : logOut,
            fillAuthData : fillAuthData,
            authentication: authentication
        };

        return AuthServiceFactory;

        function saveRegistration(registration) {

            logOut();

            return $http.post(COMMON_CONST.REGISTRATION_URL, registration).then(function (response) {
                return response;
            })
        };

        function login(loginData) {

            var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

            var deferred = $q.defer();

            $http.post('token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

                localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName });

                authentication.isAuth = true;
                authentication.userName = loginData.userName;

                deferred.resolve(response);

            }).error(function (err, status) {
                logOut();
                deferred.reject(err);
            });

            return deferred.promise;
        };

        function logOut() {
            localStorageService.remove('authorizationData');
            authentication.isAuth = false;
            authentication.userName = "";
        };

        function fillAuthData() {
            var authData = localStorageService.get('authorizationData');
            if (authData) {
                authentication.isAuth = true;
                authentication.userName = authData.userName;
            }
        }
    }
})();