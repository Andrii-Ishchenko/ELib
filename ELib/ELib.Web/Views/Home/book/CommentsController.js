(function () {
    angular.module("elib")
           .controller("CommentsController", CommentsController);

    CommentssController.$inject = ["dataServiceFactory", "$scope", '$routeParams'];
    function CommentsController(dataServiceFactory, $scope, $routeParams) {
        var vm = this;

        $scope.template = {
            main: "/views/home/comment.html"
        }


        vm.pageCount = 3;
        vm.currPage = ($routeParams.pageNumb) ? $routeParams.pageNumb : 1;
        var obj = dataServiceFactory.getService('comments').get({ pageCount: $routeParams.pageCount, pageNumb: $routeParams.pageNumb });
        obj.$promise.then(function (data) {
            vm.comments = data.comments;
        })
    }


})();
 