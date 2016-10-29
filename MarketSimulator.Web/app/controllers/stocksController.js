'use strict';
app.controller('stocksController', ['$scope', 'authService', 'userWalletService', function ($scope, authService, userWalletService) {

    $scope.liveStocks = {};

    var socket = new WebSocket('ws://webtask.future-processing.com:8068/ws/stocks');

        // Handle any errors that occur.
        socket.onerror = function (error) {
            //TODO: put some toast here.
            console.log('WebSocket Error: ' + error);
            console.log(error);
        };

        socket.onmessage = function(event) {
            var data = JSON.parse(event.data);
            
            data.Items.forEach(function(liveStocks)  {
                console.log(liveStocks);
                $scope.liveStocks[liveStocks.Code] = liveStocks;
            });

            $scope.$apply();            
        }

        var getUserStocks = function () {
            var test = userWalletService.getUserWallet(authService.authentication.userId);
            console.log(test);
            //$scope.userStocks 
        };

        getUserStocks();
}]);