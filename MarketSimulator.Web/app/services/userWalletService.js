'use strict';
app.factory('userWalletService', ['$http', function($http) {
    
    var serviceBase = 'http://localhost:51136/';
    var userWalletServiceFactory = {};

    var _getUserWallet = function() {

            return $http.get(serviceBase + 'api/UserWallet').then(function (response) {
            var data = response.data || response;
            return data;
        })};
    
    userWalletServiceFactory.getUserWallet = _getUserWallet;
 
    return userWalletServiceFactory; 
}]);


