(function () {
    angular.module('elib').controller('MainController', MainController)

    MainController.$inject = ["dataServiceFactory", "$attrs"];

    function MainController(dataServiceFactory, $attrs) {

        var vm = this;
        vm.itemsPerPage = 6;
        vm.currentPage = 1;

        //blockId = 0 for most rated books
        //blockId = 1 for the latest books
        vm.blockId = $attrs.blockId;

        //get first amount of books for block
        getBooksForBlock(vm.currentPage);


        //envoked when previous/next button is clicked
        vm.pageChanged = function () {
            getBooksForBlock(vm.currentPage);
        };

        //gets books of current page for current block 
        function getBooksForBlock(pageNumb) {
            vm.books =  dataServiceFactory.getService('books')
                             .query({ blockId: vm.blockId, pageCount: vm.itemsPerPage, pageNumb: pageNumb },
                                                onSuccess = function (response, headers) {
                                                    vm.totalItems = headers("totalCount");
                                                 },
                                                onError = function (response) {
                                                                console.log(response.status);
                                                            });
        }
    }
})();

