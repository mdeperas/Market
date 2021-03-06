var app = angular.module('MarketSimulatorApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar']);

app.config(function ($routeProvider) {

	$routeProvider.when("/home",
	{
		controller: "homeController",
		templateUrl: "/app/views/home.html"
	});

	$routeProvider.when("/login",
	{
		controller: "loginController",
		templateUrl: "/app/views/login.html"
	});

	$routeProvider.when("/signup",
	{
		controller: "signupController",
		templateUrl: "/app/views/signup.html"
	});

	$routeProvider.when("/stocks",
	{
		controller: "stocksController",
		templateUrl: "/app/views/stocks.html"
	});

	$routeProvider.otherwise({ redirectTo: "/home" });
});

app.config(['$httpProvider', function ($httpProvider) {
	$httpProvider.interceptors.push('authInterceptorService');
}]);

app.run(['authService', function (authService) {

	authService.fillAuthData();	
}]);