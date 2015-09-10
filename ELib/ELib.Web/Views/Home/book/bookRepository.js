(function () {
    angular.module("elib")
           .factory("bookRepository", bookRepository);

    bookRepository.$inject = ['$resource'];

    function bookRepository($resource) {
        var baseUrl = "/api/books/";
        var DataService = {
                getBooksForAuthor: getBooksForAuthor, getBooksForPublisher,getBookById, getBestRatingBooks,getNewBooks
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

        function getBooksForPublisher() {
            var url = baseUrl + "books-for-publisher/:id";
            return $resource(url, { id: '@id' }, {
                update: {
                    query: 'Get',
                    isArray: true
                }
            });
        }

        function getBestRatingBooks() {
            var url = baseUrl + "best-rating-books";
            return $resource(url, {
                update: {
                    query: 'Get',
                    isArray: true
                }
            });
        }

        function getNewBooks() {
            var url = baseUrl + "new-books";
            return $resource(url, {
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