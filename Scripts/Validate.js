var app = angular.module("myApp", ['ui.bootstrap','myAppDirectives']);
//app.run(function ($rootScope){ $rootScope.register = {} });

app.controller("myCtrl", function ($scope,$http,$filter) {

    

    $scope.register = {};
    $scope.login = {};
    $scope.profile = {};
    $scope.leave = {};
    $scope.LeaveData = [];
    $scope.Adminleave = {};
    $scope.Employees = {};
    $scope.History = [];
    $scope.Master = [];

    $scope.currentPage = 1;
    $scope.itemsPerPage = 4;

    $scope.today = function () {
        $scope.DateOfBirth = new Date();
    };
    $scope.today();

    $scope.dateOptions = {
        formatYear: 'yy',
        startingDay: 1,
        showWeeks: false,
        
    };

    $scope.dateOptions1 = {
        formatYear: 'yy',
        startingDay: 1,
        showWeeks: false,
        minDate: new Date()
    };

    $scope.toggleMin = function () {
        $scope.minDate1 = $scope.minDate1 ? null : new Date();
    };
    $scope.toggleMin();

    $scope.formats = ['dd-MM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
    $scope.format = $scope.formats[0];

    $scope.open = function ($event) {
        $scope.showdp = true;
    };

    $scope.open1 = function ($event) {
        $scope.showdp1 = true;
    };

    $scope.showdp = false;
    $scope.showdp1 = false;
    $scope.disabled = function (date, mode) {
        return (mode === 'day' && (date.getDay() === 0 || date.getDay() === 6));
    };

    $scope.difference = function (startdate, enddate) {

        if (startdate != null && enddate != null) {
            var days = (enddate - startdate) / (1000 * 60 * 60 * 24);
            var weeks = Math.round(days) / 7;
            var totalweekends = Math.round(weeks) * 2;
            var workingdays = Math.round(days) - totalweekends;
            $scope.leave.leaveduration = workingdays + 1;
        }
        else {
            leave.leaveduration = 0;
        }

    }
    

    $scope.Register = function () {

        $http.post("/Login/RegisterEmployee", $scope.register).success(function (response) {
            alert("Registration Successful");
            $scope.register = {};
            $scope.myForm.$setPristine();
            $scope.myForm.$setUntouched();
        }).error(function (response) {
            alert("Registrtaion Failed");
        });
    }

    $scope.Login = function () {

        $http.post("/Login/LoginEmployee", $scope.login).success(function (response) {
            
            if (response == "Login Success") {
                
                //$scope.EmployeeDetails(); 
                window.location.href = '/Employee/Dashboard?username=' + $scope.login.username;
            }
            else if (response == "Admin Success")
            {
                window.location.href = '/Admin/AdminDashboard?username=' + $scope.login.username;
            }
            else if (response == "Blocked User")
            {
                alert("Sorry! You are Blocked...");
                $scope.login = {};
                $scope.myForm.$setPristine();
                $scope.myForm.$setUntouched();
            }
            else {
                alert("Wrong Username or Password");
                $scope.login = {};
                $scope.myForm.$setPristine();
                $scope.myForm.$setUntouched();
            }
            
        }).error(function (response) {
            console.log("Login Failed");
            });
    }

    $scope.ForgetPassword = function () {

        
        $http.post("/Login/ForgetPassword", $scope.register).success(function (response) {

            if (response == "Registered Email") {
                alert("Password is sent to Email, Please check");
            }
            else {
                alert("Email is not registered");
            }
        }).error(function (response) {
            console.log("Forget Error");
        });
    }
    
    $scope.EmployeeDetails = function () {

        $http({
            method: 'GET',
            url: '/Employee/EmployeeDetails',
            params: { username: $scope.login.username }
        }).then(function (response) {
            console.log("success");
            angular.copy(response.data, $scope.register);
            $scope.GetLeaveDetails();
            //$scope.GetProfileDetails();
            //$scope.register = response.data;
            //window.location.href = '/Employee/Dashboard'
            //$location.path('/Employee/Dashboard/').replace();

        },function (response) {
            console.log("error");
        });
    }

    if (window.location.href.toLowerCase().indexOf('/dashboard') != -1) {
        $scope.login = {};
        $scope.login.username = username;
        $scope.EmployeeDetails();

    }

    $scope.AdminDetails = function () {

        $http({
            method: 'GET',
            url: '/Admin/AdminDetails',
            params: { username: $scope.login.username }
        }).then(function (response) {
            console.log("success");
            angular.copy(response.data, $scope.register);
            $scope.GetAdminLeaveDetails();
        }, function (response) {
            console.log("error");
        });
    }

    if (window.location.href.toLowerCase().indexOf('/admindashboard') != -1) {
        $scope.login = {};
        $scope.login.username = username;
        $scope.AdminDetails();

    }

    $scope.LeaveDetails = function () {

        $http.post("/Employee/ApplyForLeaveDetails", $scope.leave).success(function (response) {
            alert("Leave Applied Successfully");
            $scope.leave = {};
            $scope.myForm.$setPristine();
            $scope.myForm.$setUntouched();
            $scope.GetLeaveDetails();
        }).error(function (response) {
            console.log("Leave Error");
        });
    }

    $scope.GetLeaveDetails = function () {
    
        $http({
            method: 'GET',
            url: '/Employee/GetLeaveDetails',
        }).then(function (response) {
            console.log("success");
            //$scope.leave = response.data;
            $scope.LeaveData = response.data;
        }, function (response) {
            console.log("error");
        });
    }

    $scope.GetProfileDetails = function () {

        $http({
            method: 'GET',
            url: '/Employee/GetProfileDetails',
        }).then(function (response) {
            console.log("success");
            angular.copy(response.data, $scope.profile);
            $scope.profile.dateofbirth = $filter('jsdate')(response.data.dateofbirth);
        }, function (response) {
            console.log("error");
        });

    }

    $scope.UpdateProfileDetails = function () {

        $http.post("/Employee/UpdateProfileDetails", $scope.profile).success(function (response) {
            alert("Profile Updated Successfully");
            $scope.profile = {};
            $scope.myForm.$setPristine();
            $scope.myForm.$setUntouched();
        }).error(function (response) {
            console.log("Profile Update Error");
        });
    }

    $scope.GetAdminLeaveDetails = function () {

        $http({
            method: 'GET',
            url: '/Admin/GetAdminLeaveDetails',
        }).then(function (response) {
            console.log("success");
            $scope.Adminleave = response.data;
        }, function (response) {
            console.log("error");
        });

    }

    $scope.GetEmployeeDetails = function () {

        $http({
            method: 'GET',
            url: '/Admin/GetEmployeeDetails',
        }).then(function (response) {
            console.log("success");
            $scope.Employees = response.data;
        }, function (response) {
            console.log("error");
        });
    }

    $scope.GetHistoryDetails = function () {

        $http({
            method: 'GET',
            url: '/Admin/GetHistoryDetails',
        }).then(function (response) {
            console.log("success");
            $scope.History = response.data;
        }, function (response) {
            
            console.log("error");
        });
    }
    //////////////////////////////////// DROPDOWN FROM MASTER TABLE /////////////////////

    $scope.GetMasterData = function () {


        $http({
            method: 'GET',
            url: '/Employee/GetMasterData',
        }).then(function (response) {
            console.log("success");
            $scope.Master = response.data;
        }, function (response) {
            console.log("error");
        });

    }

    /////////////////////////// SORTING ///////////////////////

    $scope.sortBy = function (column) {


        $scope.sortColumn = column;
        $scope.reverse = !$scope.reverse;
    }

    //////////////////////////// LEAVE CANCEL,APPROVE,REJECT, BLOCK,UNBLOCK USER ///////////////////////////////////////////////

    $scope.CancelLeave = function (l) {


        $scope.leave = l;
        $scope.leave.startdate = new Date(parseInt($scope.leave.startdate.substr(6)));

        $http.post("/Employee/CancelLeave", $scope.leave).success(function (response) {
            console.log("Leave Cancelled Successfully");
            $scope.GetLeaveDetails();
        }).error(function (response) {
            console.log("Leave Cancel Error");
        });
        
    }

    $scope.ApproveLeave = function (a) {


        $scope.Adminleave = a;
        $scope.Adminleave.startdate = new Date(parseInt($scope.Adminleave.startdate.substr(6)));

        $http.post("/Admin/ApproveLeave", $scope.Adminleave).success(function (response) {
            console.log("Leave Approved Successfully");
            $scope.GetAdminLeaveDetails();
        }).error(function (response) {
            console.log("Leave Approve Error");
        });
    }

    $scope.RejectLeave = function (a) {


        $scope.Adminleave = a;
        $scope.Adminleave.startdate = new Date(parseInt($scope.Adminleave.startdate.substr(6)));


        $http.post("/Admin/RejectLeave", $scope.Adminleave).success(function (response) {
            console.log("Leave Rejected Successfully");
            $scope.GetAdminLeaveDetails();
        }).error(function (response) {
            console.log("Leave Approve Error");
        });
    }

    $scope.BlockUser = function (Emp) {

        $scope.Employees = Emp;
        $http.post("/Admin/BlockUser", $scope.Employees).success(function (response) {
            console.log("User Blocked Successfully");
            $scope.GetEmployeeDetails();
        }).error(function (response) {
            console.log("User Block Error");
        });
        
    }

    $scope.UnblockUser = function (Emp) {

        $scope.Employees = Emp;
        $http.post("/Admin/UnblockUser", $scope.Employees).success(function (response) {
            console.log("User Unblocked Successfully");
            $scope.GetEmployeeDetails();
        }).error(function (response) {
            console.log("User Unblock Error");
        });
        
    }

    
});

/////////////////////////////// CUSTOM FILTERS ////////////////////////////

  app.filter('jsdate', ['$filter', function ($filter) {
    return function (input, format) {
        return (input)
            ? $filter('date')(parseInt(input.substr(6)), format)
            : '';
    };
}]);

///////////////////////////////// CUSTOM DIRECTIVES ///////////////////

app.directive('numbersOnly', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attr, ngModelCtrl) {
            function fromUser(text) {
                if (text) {
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

    /*MY KNOWLEDGE
    app.directive('numbersOnly', function () {
      return  {
          restrict: 'A',
          link: function (scope, elm, attrs, ctrl) {
              elm.on('keydown', function (event) {
                  if(event.shiftKey)
                      {
                          event.preventDefault(); 
                          return false;
                      }
                  //console.log(event.which);
                  if ([8, 13, 27, 37, 38, 39, 40].indexOf(event.which) > -1) {
                      // backspace, enter, escape, arrows
                      return true;
                  } else if (event.which >= 48 && event.which <= 57) {
                      // numbers 0 to 9
                      return true;
                  } else if (event.which >= 96 && event.which <= 105) {
                      // numpad number
                      return true;
                  } else {
                      event.preventDefault();
                      return false;
                  }
              });
          }
      }
    });*/

    // My Knowledge
    //app.directive('validateEmail', function () {
    //    var EMAIL_REGEXP = /^[_a-z0-9]+(\.[_a-z0-9]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$/;

    //    return {
    //        require: 'ngModel',
    //        restrict: '',
    //        link: function (scope, elm, attrs, ctrl) {
    //            // only apply the validator if ngModel is present and Angular has added the email validator
    //            if (ctrl && ctrl.$validators.email) {

    //                // this will overwrite the default Angular email validator
    //                ctrl.$validators.email = function (modelValue) {
    //                    return ctrl.$isEmpty(modelValue) || EMAIL_REGEXP.test(modelValue);
    //                };
    //            }
    //        }
    //    };
    //});


    //$scope.EmployeeDetails = function () {

    //    $http.post("/Employee/EmployeeDetails", $scope.login).success(function (response) {
    //        console.log("success response");
    //        debugger;
    //        angular.copy(response, $scope.register);
    //    }).error(function (response) {
    //        debugger;
    //        console.log("Error");
    //    });

    //}

//app.filter("jsdate", function () {

//    return function (x) {
//        return new Date(parseInt(x.substr(6)));
//    };
//});

//app.config(function ($locationProvider) {

//});
