(function () {
    angular.module("elib")
           .factory("profileFactory", profileFactory);

    profileFactory.$inject = ['$resource'];

    function profileFactory($resource) {
        var baseUrl = "/api/";
        var ProfileService = {
            getCurrentUser: getCurrentUser,
        };

        return ProfileService;

        function getCurrentUser() {
            var url = baseUrl + "Profile/GetCurrentUser";

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