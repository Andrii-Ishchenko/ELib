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
            .when('/authors', {
                templateUrl: '/views/shared/two-column-layout.html',
                controller: 'AuthorsController',
                controllerAs:'authors'
            })
            .when('/author/:id', {
                templateUrl: '/views/home/author/author.html',
                controller: 'AuthorController',
                controllerAs: 'author'
            })
            .when('/login', {
                templateUrl: '/views/home/login.html',
                controller: 'LoginController',
                controllerAs: 'loginCtrl'
            })
            .when('/registration', {
                templateUrl: '/views/home/registration.html',
                controller: 'RegistrationController',
                controllerAs: 'registrationCtrl'
            })
        //.........
            .otherwise({
                redirectTo: 'books'
            });
        }
   })();

