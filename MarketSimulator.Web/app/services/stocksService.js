'use strict';
app.factory('stocksService', ['$http', function($http) {
    
    var serviceBase = 'http://localhost:51136/';
    var stocksServiceFactory = {};
 
    var _getStocks = function () {
 
    
        return $http.get(serviceBase + 'api/stock').then(function (response) {
            var data = response.data || response;
            return data;
        });
    };

    var _saveUserStocks = function(stocks) {

        var username = authService.authenication;

        $http.post(serviceBase + 'api/stock', stocks).then(function (response) {
            console.log(response);
        })
    };
    stocksServiceFactory.getStocks = _getStocks;
    stocksServiceFactory.saveUserStocks = _saveUserStocks;
 
    return stocksServiceFactory;
 
}]);


