(function () {
    angular.module("elib")
           .factory("ProfileFactory", ProfileFactory);

    ProfileFactory.$inject = ['$resource'];

    function ProfileFactory($resource) {
        var baseUrl = "/api/";
        var ProfileService = {
            getCurrentUser: getCurrentUser,
        };

        return ProfileService;

        function getCurrentUser() {
            var url = baseUrl + "/Profile/GetCurrentUser";

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