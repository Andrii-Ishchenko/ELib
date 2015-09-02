(function () {
    angular.module("elib")
           .factory("authServiceFactory", authServiceFactory);

    authServiceFactory.$inject = ['$http'];
    authServiceFactory.$inject = ['$q'];
    authServiceFactory.$inject = ['localStorageService'];

    function authServiceFactory($http, $q, localStorageService) {
        var serviceBase = '/api/';

        var AuthServiceFactory = {
            saveRegistration : saveRegistration,
            login : login,
            logOut : logOut,
            fillAuthData : fillAuthData,
            authentication : authentication
        };

        return AuthServiceFactory;

        var authentication = {
            isAuth: false,
            userName: ""
        };

        function saveRegistration(registration) {

            logOut();

            return $http.post(serviceBase + 'account/register', registration).then(function (response) {
                return response;
            });

        };

        function login(loginData) {

            var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

            var deferred = $q.defer();

            $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

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