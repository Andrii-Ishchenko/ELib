(function () {
    angular.module("ELib")
           .factory("DataServiceFactory", DataServiceFactory);

    DataServiceFactory.$inject = ['$resource'];

    function DataServiceFactory($resource) {
        var baseUrl = "/api/";
        var DataService = {
            getAll: getAll,
            getById: getById,
            add: add,
            update: update,
            deleteById: deleteById
        };

        return DataService;

        function getAll(entity) {
            var url = baseUrl  + entity + "/:id";
            return $resource(url, {}, {
                query: {
                    method: 'GET',
                    params: {},
                    isArray: true
                }
            });
        }

             
        
        function getById() {

        }

        function add() {

        }

        function update() {

        }

        function deleteById() {

        }
    }
})();