angular.module('app').controller('AdminDashboardController', function ($scope, $stateParams, $http, apiUrl, $state, TeamResource) {

    $scope.users = TeamResource.getUsersInTeam();

});