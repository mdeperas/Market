'use strict';
app.factory('stocksService', ['$http', 'authService', function($http, authService) {
    
    var serviceBase = 'http://localhost:51136/';
    var stocksServiceFactory = {};

    var userStocks = {};
 
    var _getStocks = function () {    
        return $http.get(serviceBase + 'api/stock').then(function (response) {
            var data = response.data || response;
            return data;
        });
    };

    stocksServiceFactory.getStocks = _getStocks;
 
    return stocksServiceFactory; 
}]);


