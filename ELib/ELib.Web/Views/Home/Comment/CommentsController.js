(function () {
    angular.module("elib")
           .controller("CommentsController", CommentsController);

    CommentsController.$inject = ["dataServiceFactory", "$scope", '$routeParams'];

    function CommentsController(dataServiceFactory,$scope, $routeParams) {
        var vm = this;

    $scope.template = {
        main: "/views/home/book/books.html"
    }





})();