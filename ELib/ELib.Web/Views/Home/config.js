(function () {
    angular.module("elib")
           .config(config);

    function config($routeProvider, $httpProvider, $locationProvider) {
        $routeProvider
            //.when('/', {
            //    redirectTo: 'books'
            //})
            .when('/', {
                templateUrl: '/views/home/mainpage/main.html',
                controller: 'MainPageController',
                controllerAs: 'mainpage'
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
                controller: 'CurrentProfileController',
                controllerAs: 'currentProfileCtrl'
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
            .when('/publishers', {
                templateUrl: '/views/home/publisher/publishers.html',
                controller: 'PublishersController',
                controllerAs: 'publishers'
            })
            .when('/publisher/:id', {
                templateUrl: '/views/home/publisher/publisher.html',
                controller: 'PublisherController',
                controllerAs: 'publisher'
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

