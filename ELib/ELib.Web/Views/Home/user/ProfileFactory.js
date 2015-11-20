//not in use now
(function () {
    angular.module("elib")
           .factory("profileFactory", profileFactory);

    profileFactory.$inject = ['$resource', 'USER_CONST'];

    function profileFactory($resource, USER_CONST) {
        var ProfileService = {
            getCurrentUser: getCurrentUser,
        };

        return ProfileService;

        function getCurrentUser() {
            var url = USER_CONST.USER;
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