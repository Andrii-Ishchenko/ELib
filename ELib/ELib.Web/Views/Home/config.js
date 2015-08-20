(function () {
    angular.module("ELib")
           .config(config);

function config($routeProvider) {
    $routeProvider
        .when('/books', {
            templateUrl: '/views/home/book/books.html',
          controller : 'BooksController',
          controllerAs : 'books'
      })
      //.........
      .otherwise({
          redirectTo: '/books'
      });
        }
   })();

