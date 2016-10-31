'use strict';
app.factory('userWalletService', ['$http', function($http) {
    
    var serviceBase = 'http://localhost:51136/';
    var userWalletServiceFactory = {};

    var _getUserWallet = function(onsuccess) {

            return $http.get(serviceBase + 'api/UserWallet').then(function (response) {
            var data = response.data || response;
            onsuccess(data);
        })};
    
    userWalletServiceFactory.getUserWallet = _getUserWallet;
 
    return userWalletServiceFactory; 
}]);


