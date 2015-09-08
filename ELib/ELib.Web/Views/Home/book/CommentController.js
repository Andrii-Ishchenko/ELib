(function () {
    angular.module("elib")
           .controller("CommentController", CommentController);

    CommentController.$inject = ["commentRepository", '$routeParams'];

    function CommentController(commentRepository, $routeParams) {
        var vm = this;
        vm.instance = commentRepository.getCommentById().get({ id: $routeParams.id });
        };

    }
})();