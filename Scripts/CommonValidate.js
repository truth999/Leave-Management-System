var app = angular.module("myApp.Directives",[]);

console.log("common");

app.directive('numbersOnly', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attr, ngModelCtrl) {
            function fromUser(text) {
                if (text) {
                    debugger;
                    var input = text[0];
                    if (input <= 5) {
                        var transformedInput = text.replace(/[^6-9]/g, '');
                    }
                    else {
                        var transformedInput = text.replace(/[^0-9]/g, '');
                    }
                    console.log("Transformed");

                    if (transformedInput !== text) {
                        ngModelCtrl.$setViewValue(transformedInput);
                        ngModelCtrl.$render();
                    }
                    return transformedInput;
                }
                return undefined;
            }
            ngModelCtrl.$parsers.push(fromUser);
        }
    };
});

///*MY KNOWLEDGE
//app.directive('numbersOnly', function () {
//  return  {
//      restrict: 'A',
//      link: function (scope, elm, attrs, ctrl) {
//          elm.on('keydown', function (event) {
//              if(event.shiftKey)
//                  {
//                      event.preventDefault(); 
//                      return false;
//                  }
//              //console.log(event.which);
//              if ([8, 13, 27, 37, 38, 39, 40].indexOf(event.which) > -1) {
//                  // backspace, enter, escape, arrows
//                  return true;
//              } else if (event.which >= 48 && event.which <= 57) {
//                  // numbers 0 to 9
//                  return true;
//              } else if (event.which >= 96 && event.which <= 105) {
//                  // numpad number
//                  return true;
//              } else {
//                  event.preventDefault();
//                  return false;
//              }
//          });
//      }
//  }
//});*/

app.directive("limitTo", [function () {
    return {
        restrict: "A",
        link: function (scope, elem, attrs) {
            var limit = parseInt(attrs.limitTo);
            angular.element(elem).on("keypress", function (e) {
                if (this.value.length == limit) e.preventDefault();
            });
        }
    }
}]);

app.directive('onlyAlphabets', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attr, ngModelCtrl) {
            function fromUser(text) {
                var transformedInput = text.replace(/[^A-Za-z ]/g, '');
                console.log(transformedInput);
                if (transformedInput !== text) {
                    ngModelCtrl.$setViewValue(transformedInput);
                    ngModelCtrl.$render();
                }
                return transformedInput;
            }
            ngModelCtrl.$parsers.push(fromUser);
        }
    };
});

