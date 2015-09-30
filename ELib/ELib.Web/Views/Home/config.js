(function () {
    angular.module("elib")
           .config(['$routeProvider', '$httpProvider', '$locationProvider', config]);

    function config($routeProvider, $httpProvider, $locationProvider) {
        $routeProvider
            .when('/', {
                templateUrl: '/views/home/mainpage/main.html',
                controller: 'MainController',
                controllerAs: 'mainpage'
            })
            .when('/books', {
                templateUrl: '/views/home/book/books.html',
                controller : 'BooksController',
                controllerAs : 'books'
            })
            .when('/books/new', {
                templateUrl: '/views/home/book/new-book.html',
                controller: 'NewBookController',
                controllerAs: 'newBookCtrl'
            })
             .when('/books/search', {
                 templateUrl: '/views/home/book/books.html',
                 controller: 'BooksController',
                 controllerAs: 'books'
             })
            .when('/books/:id', {
                templateUrl: '/views/home/book/book.html',
                controller: 'BookController',
                controllerAs: 'book'
            })
            .when('/profile/',{
                templateUrl: '/views/home/user/profile.html',
                controller: 'CurrentProfileController',
                controllerAs: 'currentProfileCtrl'
            })
            .when('/authors', {
                templateUrl: '/views/home/author/authors.html',
                controller: 'AuthorsController',
                controllerAs:'authors'
            })
             .when('/authors/new', {
                 templateUrl: '/views/home/author/new-author.html',
                 controller: 'NewAuthorController',
                 controllerAs: 'newAC'
             })
            .when('/authors/search', {
                templateUrl: '/views/home/author/authors.html',
                controller: 'AuthorsController',
                controllerAs: 'authors'
            })
            .when('/authors/:id', {
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
            .when('/help', {
                templateUrl: '/views/home/help/help.html',
                controller : 'HelpController',
                controllerAs : 'helpCtrl'
            })
            .when('/publishers', {
                templateUrl: '/views/home/publisher/publishers.html',
                controller: 'PublishersController',
                controllerAs: 'publishers'
            })
            .when('/publishers/search', {
                templateUrl: '/views/home/publisher/publishers.html',
                controller: 'PublishersController',
                controllerAs: 'publishers'
            })
            .when('/publishers/new', {
                templateUrl: '/views/home/publisher/new-publisher.html',
                controller: 'NewPublisherController',
                controllerAs: 'newPC'
            })
            .when('/publishers/:id', {
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

