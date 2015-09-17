(function () {
    angular.module("elib")
           .controller("FilterController", FilterController);

    FilterController.$inject = ["$location"];

    function FilterController($location) {
        vm = this;

        vm.addSelect = function () {

        };
    }
})();