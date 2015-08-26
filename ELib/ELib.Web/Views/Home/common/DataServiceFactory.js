(function () {
    angular.module("elib")
           .factory("dataServiceFactory", dataServiceFactory);

    dataServiceFactory.$inject = ['$resource'];

    function dataServiceFactory($resource) {
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
            var url = baseUrl  + entity;
            return $resource(url, {}, {
                query: {
                    method: 'GET',
                    params: {},
                    isArray: true
                }
            });
        }

             
        
        function getById(entity, id) {
            var url = baseUrl + entity + "/:id";
            return $resource(url, {}, {
                query: {
                    method: 'GET',
                    params: {id: id},
                    isArray: false
                }
            });
        }

        function add() {

        }

        function update() {

        }

        function deleteById() {

        }
    }
})();