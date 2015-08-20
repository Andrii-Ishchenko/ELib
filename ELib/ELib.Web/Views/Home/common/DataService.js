(function () {
    angular.module("Elib")
           .factory("DataService", DataService);

    DataService.$inject = ['$http'];

    function DataService($http) {
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
            var url = baseUrl + entity;
            $http.get(url).success().error();
        }
        //......
    }
})();