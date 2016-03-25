angular.module('app', ['ngResource', 'ui.router', 'LocalStorageModule']);

angular.module('app').value('apiUrl', 'http://localhost:52816/api');

angular.module('app').config(function ($stateProvider, $urlRouterProvider, $httpProvider) {
    //$httpProvider.interceptors.push('AuthenticationInterceptor');

    //$urlRouterProvider.ortherwise('/home');

    $stateProvider
        .state('home', { url: '/home', templateUrl: '/templates/home/home.html', controller: 'HomeController' })
        .state('adminRegister', { url: '/adminRegister', templateUrl: '/templates/home/register/adminRegister.html', controller: 'AdminRegisterController' })
        .state('inviteRegister', { url: '/inviteRegister/:token', templateUrl: '/templates/home/register/inviteRegister.html', controller: 'InviteRegisterController' })

        .state('app', { url: '/app', templateUrl: '/templates/app/app.html', controller: 'AppController' })
            .state('app.admindashboard', { url: '/admindashboard', templateUrl: '/templates/app/admindashboard/admindashboard.html', controller: 'AdminDashboardController' })
            .state('app.userdashboard', { url: '/userdashboard', templateUrl: '/templates/app/userdashboard/userdashboard.html', controller: 'UserDashboardController' })
            .state('app.createteam', { url: '/createteam', templateUrl: '/templates/app/createteam/createteam.html', controller: 'CreateTeamController' })
            .state('app.createorder', { url: '/createorder', templateUrl: '/templates/app/createorder/createorder.html', controller: 'CreateOrderController' })
            .state('app.team', { url: '/team', templateUrl: '/templates/app/team/team.html', controller: 'TeamController' })
            .state('app.currentorders', { url: '/currentorders', templateUrl: '/templates/app/currentorders/currentorders.html', controller: 'CurrentOrdersController' })
            .state('app.pastorders', { url: '/pastorders', templateUrl: '/templates/app/pastorders/pastorders.html', controller: 'PastOrdersController' });
});

//angular.module('app').run(function (AuthenticationService) {
//    AuthenticationService.initialize();
//});