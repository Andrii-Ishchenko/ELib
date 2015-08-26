(function () {
    angular.module("elib")
           .config(config);

function config($routeProvider) {
    $routeProvider
        .when('/books', {
            templateUrl: '/views/home/book/books.html',
          controller : 'BooksController',
          controllerAs : 'books'
        })
        .when('/book/:id', {
            templateUrl: '/views/home/book/book.html',
            controller: 'BookController',
            controllerAs: 'book'
        })
      //.........
      .otherwise({
          redirectTo: '/books'
      });
        }
   })();

