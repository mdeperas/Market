'use strict';
app.controller('signupController', ['$scope', '$location', '$timeout', 'authService', 'stocksService', function ($scope, $location, $timeout, authService, stocksService) {
 
    $scope.savedSuccessfully = false;
    $scope.message = "";
    $scope.stocks = [];
 
    $scope.registration = {
        userName: "",
        password: "",
        confirmPassword: "",
    };

    $scope.userdata = {};

    stocksService.getStocks().then(function (results) {
        $scope.stocks = results;
    }, function (error) {
        console.log(error.errors);
    })
 
    $scope.signUp = function () {
 
        authService.saveRegistration($scope.registration).then(function (response) {
 
            $scope.stocks;
            $scope.savedSuccessfully = true;
            $scope.message = "User has been registered successfully, you will be redicted to login page in 2 seconds.";
            startTimer();
            
            /* */
            
            /*stocksService.saveUserStocks($scope.stocks).then(function (response) {
                console.log(response);
            });*/


        },
         function (response) {
             var errors = [];
             for (var key in response.data.modelState) {
                 for (var i = 0; i < response.data.modelState[key].length; i++) {
                     errors.push(response.data.modelState[key][i]);
                 }
             }
             $scope.message = "Failed to register user due to:" + errors.join(' ');
         });
    };
 
    var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $location.path('/login');
        }, 2000);
    } 
}]);