angular.module('app').controller('UserDashboardController', function ($scope, $stateParams, $http, apiUrl, $state, TeamResource) {

    $scope.users = TeamResource.getUsersInTeam();

});