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
            var url = COMMON_CONST.BASE_URL + entity + "/:id/:property";
            return $resource(url, {id: '@id', property : '@property'}, {
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