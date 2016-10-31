'use strict';
app.controller('signupController', ['$scope', '$location', '$timeout', 'authService', 'stocksService', 'userDataService', 'userWalletService', function ($scope, $location, $timeout, authService, stocksService, userDataService, userWalletService) {

    $scope.savedSuccessfully = false;
    $scope.message = "";
    $scope.stocks = {
        name: "",
        code: "",
        unit: "",
        value: ""
    };

    $scope.registration = {
        userName: "",
        password: "",
        confirmPassword: "",
        userWallets: [],
        userData: {}
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

        $scope.registration.userWallets = getUserWallet($scope.stocks);
        $scope.registration.userData = $scope.userData;

        authService.saveRegistration($scope.registration).then(function (response) {

            var userId = response.data;
            $scope.savedSuccessfully = true;
            $scope.message = "User has been registered successfully, you will be redirected to login page in 2 seconds.";
            startTimer();

            /*userDataService.saveUserData($scope.userData).then(function (response) {
                userWalletService.saveUserWallet($scope.stocks, userId).then(function (response) {
                    console.log(response);
                });
            });*/
        }, function (response) {
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

    var getUserWallet = function(stocks) {
        var userStocks = [];
        for(var index = 0; index < stocks.length; ++index)
        {
            var userStock = {
                    stockId: stocks[index].id,
                    amount: 0
                };  
  
            userStock.amount = stocks[index].value || 0;
            userStocks.push(userStock);   
        };
        return userStocks;
    };
}]);