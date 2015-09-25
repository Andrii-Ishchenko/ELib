(function () {
    angular.module("elib").controller('RatingController', RatingController)
 
    RatingController.$inject = ["authServiceFactory", "dataServiceFactory", "$location", "$timeout"];
   
    function RatingController(authServiceFactory, dataServiceFactory) {

        var vm = this;
        vm.rating = 40;
        vm.basedOn = 100;
        vm.starsCount = 5;
        vm.iconClass = 'fa fa-star';
        vm.editableRating = true;
        vm.showGrade = true;
        vm.change = function () {
            createRating();
            alert('fffff');
        };

        vm.message = '';
        vm.submitState = false;
        vm.createdSuccessfully = false;

         var createRating = function () {
            vm.submitState = true;

            var rating = {
                ValueRating: vm.rating,
                UserId:8,
                BookId:4
            }

            dataServiceFactory.getService('Ratings').save(rating).$promise.then(
               //success
               function (value) {
                   vm.createdSuccessfully = true;
                   vm.message = "Publisher has been created successfully, you will be redicted to publisher page in 2 seconds.";
                                 },
               //error
               function (error) {
                   vm.submitState = false;
                   vm.message = "Publisher creation is failed";
               }
          );
        };

        
    }
          
})();
