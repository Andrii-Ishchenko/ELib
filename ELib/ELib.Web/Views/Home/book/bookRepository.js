(function () {
    angular.module("elib")
           .factory("bookRepository", bookRepository);

    bookRepository.$inject = ['$resource'];

    function bookRepository($resource) {
        var baseUrl = "/api/books/";
        var DataService = {
            getBooksForAuthor: getBooksForAuthor,
            getBooksForPublisher: getBooksForPublisher,
            getBookById: getBookById
        };

        return DataService;


        function getBooksForAuthor() {
            var url = "/api/authors/id/books";
            return $resource(url, { id: '@id' }, {
                update: {
                    query: 'Get',
                    isArray: true
                }
            });
        }

        function getBooksForPublisher() {
            var url = "/api/publishers/id/books";
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