(function () {
    angular.module("elib")
           .factory("regServiceFactory", regServiceFactory);

    function regServiceFactory($http, $q) {
        return {
            IsUserNameAvailablle: function (userName) {
                // Get the deferred object
                var deferred = $q.defer();
                // Initiates the AJAX call
                $http({ method: 'GET', url: window.location.origin + '/api/account/IsUserNameAvailable?userName=' + userName })
                    .success(deferred.resolve)
                    .error(deferred.reject);
                // Returns the promise - Contains result once request completes
                return deferred.promise;
            },
            IsEmailAvailablle: function (email) {
                // Get the deferred object
                var deferred = $q.defer();
                // Initiates the AJAX call
                $http({ method: 'GET', url: window.location.origin + '/api/account/IsEmailAvailable?email=' + email })
                    .success(deferred.resolve)
                    .error(deferred.reject);
                // Returns the promise - Contains result once request completes
                return deferred.promise;
            }
        }
    }
})();