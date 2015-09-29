(function () {
    angular.module("elib")
           .factory("currentProfileFactory", currentProfileFactory);

    currentProfileFactory.$inject = ['$resource'];

    function currentProfileFactory($resource) {
        var baseUrl = "/api/";
        var CurrentProfileService = {
            getCurrentUser: getCurrentUser,
            saveCurrentUser: saveCurrentUser
        };

        return CurrentProfileService;

        function getCurrentUser() {
            var url = baseUrl + "CurrentProfile";

            return $resource(url, {}, {
                query: {
                    method: 'GET',
                    params: {},
                    isArray: false
                }
            });
        }

        function saveCurrentUser(user) {
            var url = baseUrl + "CurrentProfile";

            return $resource(url, {}, {
                send: {
                    method: 'PUT',
                    data:user
                }
            });
        }

    }

})();