(function () {
    angular.module("elib")
           .factory("bookRepository", bookRepository);

    bookRepository.$inject = ['$resource'];

    function bookRepository($resource) {
        var baseUrl = "/api/books/";
        var DataService = {
            getBooksForAuthor: getBooksForAuthor, getBookById
        };

        return DataService;


        function getBooksForAuthor() {
            var url = baseUrl + "books-for-author/:id";
            return $resource(url, { id: '@id' }, {
                update: {
                    query: 'Get',
                    isArray: true
                }
            });
        }

        function getBookById() {
            var url = baseUrl + "book/:id";
            return $resource(url, { id: '@id' }, {
                update: {
                    query: 'Get',
                    isArray: false
                }
            });
        }

    }
})();