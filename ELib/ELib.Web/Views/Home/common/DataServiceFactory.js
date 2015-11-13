(function () {
    angular.module("elib")
           .factory("dataServiceFactory", dataServiceFactory);

    dataServiceFactory.$inject = ['$resource', 'COMMON_CONST'];

    function dataServiceFactory($resource, COMMON_CONST) {
        var DataService = {
            getService : getService
        };

        return DataService;

        function getService(entity) {
            var url = COMMON_CONST.BASE_URL + entity + COMMON_CONST.URL_ID;
            return $resource(url, {id: '@id'}, {
                update: {
                    method: 'PUT',
                    isArray: false
                },

                getWithTotalCount: {
                    method: 'GET',
                    isArray: false,
                }
            });
        }
    }
})();