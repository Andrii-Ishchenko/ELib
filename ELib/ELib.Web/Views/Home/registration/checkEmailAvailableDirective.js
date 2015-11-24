(function () {
    angular.module("elib")
        .directive('emailavail', emailavail);

    function emailavail(regServiceFactory) {
        var directive = {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attrs, ngModel) {
                element.on('blur', function (evt) {

                    if (!ngModel || !element.val()) return;
                    var curValue = element.val();

                    regServiceFactory.IsEmailAvailablle(curValue)
                    .then(function (response) {

                        ngModel.$setValidity('unique', response);
                    }, function () {
                        ngModel.$setValidity('unique', true);
                    });
                });
            }
        }
        return directive;
    };
})();
