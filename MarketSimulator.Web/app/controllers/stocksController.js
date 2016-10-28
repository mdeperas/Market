'use strict';
app.controller('stocksController', ['$scope', function ($scope) {

    $scope.stocks = {};

    var socket = new WebSocket('ws://webtask.future-processing.com:8068/ws/stocks');

        // Handle any errors that occur.
        socket.onerror = function (error) {
            //TODO: put some toast here.
            console.log('WebSocket Error: ' + error);
            console.log(error);
        };

        socket.onmessage = function(event) {
            var data = JSON.parse(event.data);
            
            data.Items.forEach(function(stocks)  {
                console.log(stocks);
                $scope.stocks[stocks.Code] = stocks;
            });

            $scope.$apply();            
        }
}]);