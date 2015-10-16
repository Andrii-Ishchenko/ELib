(function () {
    angular.module("elib")
           .controller("BooksController", BooksController);

    BooksController.$inject = ["dataServiceFactory", '$routeParams', "$location"];

    function BooksController(dataServiceFactory, $routeParams, $location) {
        var vm = this;
        vm.pageCount = ($routeParams.pageCount) ? $routeParams.pageCount : 5;
        vm.currPage = ($routeParams.pageNumb) ? $routeParams.pageNumb : 1;
        
        vm.alrt = function () {
             alert(vm.ordering.orderBy + " " + vm.ordering.orderDirection);
            //console.log("alert callback"); 
        }


        vm.status = {
            isFirstOpen: true
               };
       

        var catParameters = {
            isNested:true
        }

        var obj = dataServiceFactory.getService('category').query(catParameters);
        obj.$promise.then(function (data) {
            vm.categories = data;
            preProcessCategories(vm.categories,0);
        });

        vm.ToggleNode = function ToggleNode(node) {       
            if (node && node.opened != undefined)
                node.opened = !node.opened
        }

       

        vm.ordering ={
            orderBy: ($routeParams.orderBy) ? $routeParams.orderBy : 'Genre',
            orderDirection: ($routeParams.orderDirection) ? $routeParams.orderDirection : 'DESC',
            defaultOrder: "AuthorName",
            defaultDirection:"DESC",
            orderParameters: ["Title", "Year", "AuthorName", "Genre", "Publisher", "Rating", "Date"]
        }
        //vm.orderByParam = ($routeParams.orderBy) ? $routeParams.orderBy : 'Genre';
        //vm.orderDirectionParam = ($routeParams.orderDirection) ? $routeParams.orderDirection : 'DESC';
        //vm.orderParameters = ["Title", "Year", "AuthorName", "Genre", "Publisher", "Rating", "Date"];

        var parameters = {
            pageCount : vm.pageCount,
            pageNumb  : vm.currPage,
            query     : $routeParams.query,
            title     : $routeParams.title,
            authorName: $routeParams.author,
            genre     : $routeParams.genre,
            genreId   : $routeParams.genreId,
            subgenre  : $routeParams.subgenre,
            year      : $routeParams.year,
            orderBy   : vm.ordering.orderBy,
            orderDirection: vm.ordering.orderDirection
    }

        vm.pageChanged = pageChanged;

        //$location.search("pageNumb", vm.currPage);

        var obj = dataServiceFactory.getService('books').get(parameters);
        obj.$promise.then(function (data) {
            vm.books = data.books;
            vm.totalCount = data.totalCount;
            vm.totalPages = Math.ceil(vm.totalCount / vm.pageCount);
            vm.pages = new Array(vm.totalPages);
        })
      
        function pageChanged() {
            $location.search({ "pageCount": vm.pageCount, "pageNumb": vm.currPage });
        }

        vm.NodeButtonState = function (node) {

        }
      
        function preProcessCategories(children, level) {

            for (var index in children) {
                if (children[index].Level!=undefined && children[index].Level >= level) {
                    children[index].opened = false;
                } else {
                    children[index].opened = true;
                }
               
                preProcessCategories(children[index].Children,level)
            }
        }

    }
})();