'use strict';
app.factory('userDataService', ['$http', function($http) {
    
    var serviceBase = 'http://localhost:51136/';
    var userDataServiceFactory = {};
 
    var _saveUserData = function(userData) {
           
        return $http.post(serviceBase + 'api/userData', userData).then(function (response) {
            return data;
        })
    };

    userWalletServiceFactory.saveUserData = _saveUserData;
 
    return userDataServiceFactory; 
}]);


