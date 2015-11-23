(function () {
    'use strict';

    angular
        .module('elib')
        .directive('commentsDirective', commentsDirective);

    function commentsDirective($timeout){
        return {
            restrict: 'E',
            scope: { },
            controller: CommentsController,
            controllerAs : "commentsCtrl",
            link: function (scope, element, attrs) { },
            templateUrl: "/views/home/comments/Comments.html"
        }
    }

    CommentsController.$inject = ["$routeParams", "dataServiceFactory", 'BOOK_CONST', "authServiceFactory"];
    function CommentsController($routeParams, dataServiceFactory, BOOK_CONST, authServiceFactory) {
        var vm = this;
        vm.IsPostEnabled = true;
        vm.IsPostRefresh = true;
        vm.CanPost = function () {
            if (vm.IsPostEnabled && vm.newComment.Text.length > 0 && vm.newComment.Text.length < BOOK_CONST.MAX_TEXT)
                return true;
            else
                return false;
        };

        vm.CanLoad = function () {
            if (vm.temp.length < BOOK_CONST.LENGTH) {
                vm.IsPostRefresh = false;
                return true;
            }
            else
                return false;
        };

        if (authServiceFactory.authentication.isAuth) {
            vm.canLike = true;
        }
        else { vm.canLike = false; }

        //GET api/CurrentProfile
        vm.profile = dataServiceFactory.getService("CurrentProfile").get();
        var userCurrId = vm.profile.Id;

        var currentFetchedPageOfComments = BOOK_CONST.CURRENT_COMMENTS;
        var countOfFetchComments = BOOK_CONST.COUNT_COMMENTS;

        //GET api/books/id/comments?pageCount=&pageNumb=
        vm.comments = dataServiceFactory.getService("books").query({ id: $routeParams.id, property: "comments", pageCount: countOfFetchComments, pageNumb: currentFetchedPageOfComments });

        //GET api/books/id/comments?pageCount=&pageNumb=
        vm.temp = dataServiceFactory.getService("books").query({ id: $routeParams.id, property: "comments", pageCount: countOfFetchComments, pageNumb: currentFetchedPageOfComments });

        vm.newComment = {
            Text: "",
            BookId: $routeParams.id,
            UserId: null,
            CommentDate: null,
            SumLike: 0,
            SumDisLike: 0,
            UserName: "",
            State: "Added"
        };

        vm.cleanComment = function () {
            vm.newComment.Text = "";
        };

        vm.like = function (commentId) {
            createLikeOrDisLike(1, commentId);
        };
        vm.disLike = function (commentId) {
            createLikeOrDisLike(0, commentId);
        };

        vm.fetchComments = function () {
            currentFetchedPageOfComments = currentFetchedPageOfComments + 1;

            //GET api/books/id/comments?pageCount=&pageNumb=
            dataServiceFactory.getService("books").query({ id: $routeParams.id, property: "comments", pageCount: countOfFetchComments, pageNumb: currentFetchedPageOfComments }).$promise.then(
                 function (value) {
                     vm.temp = value;
                     var iterator = vm.temp.length;
                     for (var i = 0; i < iterator; i++) {
                         vm.comments.push(vm.temp[i]);
                     }
                 }
                );
        };

        var createLikeOrDisLike = function (isLike, commentId) {
            var ratingComment = {
                CommentId: commentId,
                UserId: vm.profile.Id,
                IsLike: isLike,
                State: "Added"
            }

            //POST api/RatingsComment
            dataServiceFactory.getService('RatingsComment').save(ratingComment).$promise.then(
             //success
             function (value) {

                 //GET api/books/id/comments
                 vm.comments = dataServiceFactory.getService("books").query({ id: $routeParams.id, property : "comments" });
                 vm.temp = vm.comments;
                 currentFetchedPageOfComments = BOOK_CONST.CURRENT_COMMENTS;
                 countOfFetchComments = BOOK_CONST.COUNT_COMMENTS;
                 vm.newComment.Text = "";
                 vm.IsPostEnabled = true;
                 vm.CanLoad();
             }
        );
        };

        vm.createComment = function () {
            vm.IsPostEnabled = false;
            vm.newComment.CommentDate = new Date();
            var userIdCmnt = vm.profile.Id;
            vm.newComment.UserId = userIdCmnt;
            vm.newComment.UserName = vm.profile.UserName;
            vm.newComment.State = 0;
            dataServiceFactory.getService('Comments').save(vm.newComment).$promise.then(
                 //success
                 function (value) {
                     //all???
                     //GET api/books/id/comments
                     vm.comments = dataServiceFactory.getService("books").query({ id: $routeParams.id, property: "comments" });
                     vm.temp = vm.comments;
                     currentFetchedPageOfComments = BOOK_CONST.CURRENT_COMMENTS;
                     countOfFetchComments = BOOK_CONST.COUNT_COMMENTS;
                     vm.newComment.Text = "";
                     vm.IsPostEnabled = true;
                     vm.CanLoad();
                 },
                 //error
                 function (error) {
                     vm.newComment.Text = "";
                     vm.IsPostEnabled = true;
                 }
            );
        };
    }
})();