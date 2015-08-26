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
        .when('/profile/',{
            templateUrl: '/views/home/user/profile.html',
            controller: 'ProfileController',
            controllerAs: 'profile'
        })
      //.........
      .otherwise({
          redirectTo: '/books'
      });
        }
   })();

