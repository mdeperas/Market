'use strict';
app.factory('userDataService', ['$http', function($http) {
    
    var serviceBase = 'http://localhost:51136/';
    var userDataServiceFactory = {};
 
    var _saveUserData = function(userData) {
           
        return $http.post(serviceBase + 'api/userData', userData);
    };

    userDataServiceFactory.saveUserData = _saveUserData;
 
    return userDataServiceFactory; 
}]);


