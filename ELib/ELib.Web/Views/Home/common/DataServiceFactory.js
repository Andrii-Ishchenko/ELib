(function () {
    angular.module("elib")
           .factory("dataServiceFactory", dataServiceFactory);

    dataServiceFactory.$inject = ['$resource'];

    function dataServiceFactory($resource) {
        var DataService = {
            getService : getService
        };

        return DataService;

        function getService(entity) {
            var url = "/api/" + entity + "/:id/";
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