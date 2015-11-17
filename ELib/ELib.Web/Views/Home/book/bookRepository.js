(function () {
    angular.module("elib")
           .factory("bookRepository", bookRepository);

    bookRepository.$inject = ['$resource', 'BOOK_CONST'];

    function bookRepository($resource, BOOK_CONST) {
        var DataService = {
            getBooksForAuthor: getBooksForAuthor,
            getBooksForPublisher: getBooksForPublisher,
            getBookById: getBookById
        };

        return DataService;


        function getBooksForAuthor() {
            var url = BOOK_CONST.AUTHOR_BOOK;
            return $resource(url, { id: '@id' }, {
                update: {
                    query: 'Get',
                    isArray: true
                }
            });
        }

        function getBooksForPublisher() {
            var url = BOOK_CONST.PUBLISHER_BOOK;
            return $resource(url, { id: '@id' }, {
                update: {
                    query: 'Get',
                    isArray: true
                }
            });
        }
          
        function getBookById() {
            var url = BOOK_CONST.BOOK_BY_ID;
            return $resource(url, { id: '@id' }, {
                update: {
                    query: 'Get',
                    isArray: false
                }
            });
        }
    }
})();