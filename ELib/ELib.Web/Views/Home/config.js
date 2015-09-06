(function () {
    angular.module("elib")
           .config(config);

    function config($routeProvider, $httpProvider, $locationProvider) {
        $routeProvider
            .when('/', {
                redirectTo: 'books'
            })
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
                templateUrl: '/views/home/login/login.html',
                controller: 'LoginController',
                controllerAs: 'loginCtrl'
            })
            .when('/registration', {
                templateUrl: '/views/home/registration/registration.html',
                controller: 'RegistrationController',
                controllerAs: 'registrationCtrl'
            })
        //.........
            .otherwise({
                templateUrl: '/views/home/errors/404.html',
            });

            $httpProvider.interceptors.push('authInterceptorServiceFactory');
            $locationProvider.html5Mode({
                enabled: true,
            });
        }
   })();

