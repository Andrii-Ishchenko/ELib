(function () {
    angular.module("elib")
           .factory("currentProfileFactory", currentProfileFactory);

    currentProfileFactory.$inject = ['$resource', 'USER_CONST'];

    function currentProfileFactory($resource, USER_CONST) {
        var CurrentProfileService = {
            getCurrentUser: getCurrentUser,
            saveCurrentUser: saveCurrentUser
        };

        return CurrentProfileService;

        function getCurrentUser() {
            var url = USER_CONST.CURR_PROFILE;
            return $resource(url, {}, {
                query: {
                    method: 'GET',
                    params: {},
                    isArray: false
                }
            });
        }

        function saveCurrentUser(user) {
            var url = USER_CONST.CURR_PROFILE;
            return $resource(url, {}, {
                send: {
                    method: 'PUT',
                    data:user
                }
            });
        }
    }
})();