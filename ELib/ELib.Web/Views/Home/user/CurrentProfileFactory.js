(function () {
    angular.module("elib")
           .factory("CurrentProfileFactory", CurrentProfileFactory);

    CurrentProfileFactory.$inject = ['$resource'];

    function CurrentProfileFactory($resource) {
        var baseUrl = "/api/";
        var CurrentProfileService = {
            getCurrentUser: getCurrentUser,
        };

        return CurrentProfileService;

        function getCurrentUser() {
            var url = baseUrl + "CurrentProfile/GetCurrentUser";

            return $resource(url, {}, {
                query: {
                    method: 'GET',
                    params: {},
                    isArray: false
                }
            });
        }


    }

})();