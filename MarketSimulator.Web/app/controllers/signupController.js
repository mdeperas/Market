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

    $scope.userData = {
        amountOfMoney: 0
    };

    stocksService.getStocks().then(function (results) {
        $scope.stocks = results;
    }, function (error) {
        console.log(error.errors);
    })
 
    $scope.signUp = function () {
 
        authService.saveRegistration($scope.registration).then(function (response) {
 
            var userId = response.data.id;

            $scope.savedSuccessfully = true;
            $scope.message = "User has been registered successfully, you will be redire  cted to login page in 2 seconds.";
            startTimer();

        }, function (response) {
             var errors = [];
             for (var key in response.data.modelState) {
                 for (var i = 0; i < response.data.modelState[key].length; i++) {
                     errors.push(response.data.modelState[key][i]);
                 }
             }
             $scope.message = "Failed to register user due to:" + errors.join(' ');
        });
     
            userDataService.saveUserData($scope.userData).then(function (response) {
                console.log(respone);
            });
        
            userWalletService.saveUserWallet($scope.stocks, userId).then(function (response) {
                console.log(response);
            })}  ;
          
 
    var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $location.path('/login');
        }, 2000);
    } 
}]);