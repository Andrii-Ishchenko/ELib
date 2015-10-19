angular.module("elib")
       .directive("libEnterPress", libEnterPress);

function libEnterPress() {

    return {
        restrict: "A",
        link : onEvent
    }
}

onEvent.$inject = ["$scope", "$element", "$attrs"];
function onEvent($scope, $element, $attrs) {

    $element.bind("keypress", onEnterPress);
    function onEnterPress(event) {
        var keyCode = event.which || event.keyCode;
        if (keyCode === 13) {
            $scope.$apply($attrs.libEnterPress);
            event.preventDefault();
        }
    }
}


