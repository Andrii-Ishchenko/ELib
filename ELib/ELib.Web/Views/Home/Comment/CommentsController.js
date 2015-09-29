(function () {
    angular.module("elib")
           .controller("CommentsController", CommentsController);

    CommentsController.$inject = ["commentsRepository", '$routeParams'];

    function CommentsController(commentsRepository, $routeParams) {
        var vm = this;
        vm.instance = commentsRepository.getCommentById().get({ id: $routeParams.id });
        };

    }
})();