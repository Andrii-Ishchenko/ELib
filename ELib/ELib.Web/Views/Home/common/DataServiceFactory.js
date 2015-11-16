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
                }
            });
        }


    //    function getAll(entity) {
    //        var url = baseUrl  + entity;
    //        return $resource(url, {}, {
    //            query: {
    //                method: 'GET',
    //                params: {},
    //                isArray: true
    //            }
    //        });
    //    }

             
        
    //    function getById(entity, id) {
    //        var url = baseUrl + entity + "/:id";
    //        return $resource(url, {}, {
    //            query: {
    //                method: 'GET',
    //                params: {id: id},
    //                isArray: false
    //            }
    //        });
    //    }


        function add(entity) {
            var url = baseUrl + entity;
            return $resource(url, {}, {
                save: {
                    method: 'POST',
                    isArray: false
                }
            });
        }

    //    function update(entity) {
    //        var url = baseUrl + entity;
    //        return $resource(url, {}, {
    //            update: {
    //                method: 'PUT',
    //                isArray: false
    //            }
    //        });
    //    }

    //    function deleteById(entity, id) {
    //        var url = baseUrl + entity;
    //        return $resource(url, {}, {
    //            remove : {
    //                method: 'DELETE',
    //                params: { id: id },
    //                isArray: false
    //            }
    //        });
    //    }
    }
})();