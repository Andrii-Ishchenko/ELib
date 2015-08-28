(function () {
    angular.module("elib")
           .config(config);

function config($routeProvider) {
    $routeProvider
        .when('/books', {
            templateUrl: '/views/shared/two-column-layout.html',
          controller : 'BooksController',
          controllerAs : 'books'
        })
        .when('/book/:id', {
            templateUrl: '/views/home/book/book.html',
            controller: 'BookController',
            controllerAs: 'book'
        })
        .when('/profile/',{
            templateUrl: '/views/shared/two-column-layout.html',
            controller: 'ProfileController',
            controllerAs: 'profileCtrl'
        })
      //.........
      .otherwise({
          redirectTo: '/books'
      });
        }
   })();

