angular.module('app', ['ngResource', 'ui.router', 'LocalStorageModule']);

angular.module('app').value('apiUrl', 'http://localhost:52816/api');

angular.module('app').config(function ($stateProvider, $urlRouterProvider, $httpProvider) {
    $httpProvider.interceptors.push('AuthenticationInterceptor');

    $urlRouterProvider.otherwise('/home');

    $stateProvider
        .state('home', { url: '/home', templateUrl: '/templates/home/home.html', controller: 'HomeController' })
        .state('adminRegister', { url: '/adminRegister', templateUrl: '/templates/app/register/adminRegister.html', controller: 'AdminRegisterController' })
        .state('inviteRegister', { url: '/inviteRegister/:token', templateUrl: '/templates/app/register/inviteRegister.html', controller: 'InviteRegisterController' })

        .state('app', { url: '/app', templateUrl: '/templates/app/app.html', controller: 'AppController' })
            .state('app.dashboard', { url: '/dashboard', templateUrl: '/templates/app/dashboard/adminDashboard.html', controller: 'AdminDashboardController' })
            .state('app.userdashboard', { url: '/userdashboard', templateUrl: '/templates/app/dashboard/userDashboard.html', controller: 'UserDashboardController' })
            .state('app.createorder', { url: '/createorder', templateUrl: '/templates/app/order/createOrder.html', controller: 'CreateOrderController' })
            .state('app.currentorders', { url: '/currentorders', templateUrl: '/templates/app/order/currentOrders.html', controller: 'CurrentOrdersController' })
            .state('app.pastorders', { url: '/pastorders', templateUrl: '/templates/app/order/pastOrders.html', controller: 'PastOrdersController' })
            .state('app.createteam', { url: '/createteam', templateUrl: '/templates/app/team/createteam.html', controller: 'CreateTeamController' })
            .state('app.team', { url: '/team', templateUrl: '/templates/app/team/team.html', controller: 'TeamController' });
});

angular.module('app').run(function (AuthenticationService) {
    AuthenticationService.initialize();
});