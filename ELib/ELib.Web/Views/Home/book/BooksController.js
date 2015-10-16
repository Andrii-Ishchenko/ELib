(function () {
    angular.module("elib")
           .controller("BooksController", BooksController);

    BooksController.$inject = ["dataServiceFactory", '$routeParams', "$location"];

    function BooksController(dataServiceFactory, $routeParams, $location) {
        var vm = this;
        vm.pageCount = ($routeParams.pageCount) ? $routeParams.pageCount : "5";
        vm.currPage = ($routeParams.pageNumb) ? $routeParams.pageNumb : 1;

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


        var parameters = getParameters()

        vm.pageChanged = pageChanged;

        var obj = dataServiceFactory.getService('books').get(parameters)
            .$promise.then(function (data) {
            vm.books = data.books;
            vm.totalCount = data.totalCount;
            vm.totalPages = Math.ceil(vm.totalCount / vm.pageCount);
            vm.pages = new Array(vm.totalPages);
        })
      
        function getParameters() {
            return {
                pageCount: vm.pageCount,
                pageNumb: vm.currPage,
                query: $routeParams.query,
                title: $routeParams.title,
                authorName: $routeParams.authorName,
                genre: $routeParams.genre,
                genreId: $routeParams.genreId,
                subgenre: $routeParams.subgenre,
                subgenreId: $routeParams.subgenreId,
                year: $routeParams.year
            };
        }
        function pageChanged() {
            $location.search(getParameters());
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


        //activate();

        //function activate() {
        //    return getBooks().then(function () {
        //        console.log('Activated Books View');
        //    });
        //}

        //function getBooks() {
        //    return DataService.getAll('book')

        //    .then(function (data) {
        //        vm.books = data;
        //        console.log(vm.books);
        //        return vm.books;
        //    });
        //}
    }
})();