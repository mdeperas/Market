'use strict';
app.factory('userWalletService', ['$http', function($http) {
    
    var serviceBase = 'http://localhost:51136/';
    var userWalletServiceFactory = {};

    var userStocks = [];
 
    var _saveUserWallet = function(stocks, userId) {
        for(var index = 0; index < stocks.length; ++index)
        {
            if(stocks[index].hasOwnProperty('value'))
            {
                var userStock = {
                    userId: userId,
                    stockId: stocks[index].id,
                    amount: stocks[index].value
                };          

                userStocks.push(userStock);
            }          
    };

        return $http.post(serviceBase + 'api/userWallet', userStocks).then(function (response) {
            return data;
        })};
    
    userWalletServiceFactory.saveUserWallet = _saveUserWallet;
 
    return userWalletServiceFactory; 
}]);


