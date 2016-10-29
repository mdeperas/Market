'use strict';
app.factory('userWalletService', ['$http', function($http) {
    
    var serviceBase = 'http://localhost:51136/';
    var userWalletServiceFactory = {};

    var userStocks = {};
 
    var _saveUserWallet = function(stocks, userId) {
        stocks.foreach(function(stock) {
            if(stock.value !== null)
            {
                var userStock = {
                    userId: userId,
                    stockId: stock.id,
                    amount: stock.value
                };          

                userStocks.push(userStock);
            }            
        });

        return $http.post(serviceBase + 'api/userWallet', userStocks).then(function (response) {
            return data;
        })
    };

    userWalletServiceFactory.saveUserWallet = _saveUserWallet;
 
    return userWalletServiceFactory; 
}]);


