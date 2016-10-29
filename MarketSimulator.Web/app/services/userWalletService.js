'use strict';
app.factory('userWalletService', ['$http', function($http) {
    
    var serviceBase = 'http://localhost:51136/';
    var userWalletServiceFactory = {};
    var userStocks = [];
 
    var _saveUserWallet = function(stocks, userId) {
        for(var index = 0; index < stocks.length; ++index)
        {
            var userStock = {
                    userDataId: userId,
                    stockId: stocks[index].id,
                };  

            stocks[index].hasOwnProperty('value') ? userStock.amount = stocks[index].value : userStock.amount = 0;   
            userStocks.push(userStock);   
        };

        return $http.post(serviceBase + 'api/userWallet', userStocks);
    };

    var _getUserWallet = function(userId) {

            return $http.get(serviceBase + 'api/userWallet/' + userId).then(function (response) {
            var data = response.data || response;
            return data;
        })};
    
    userWalletServiceFactory.saveUserWallet = _saveUserWallet;
    userWalletServiceFactory.getUserWallet = _getUserWallet;
 
    return userWalletServiceFactory; 
}]);


