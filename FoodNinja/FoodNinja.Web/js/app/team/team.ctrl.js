angular.module('app').controller('TeamController', function ($scope, $stateParams, $http, apiUrl, $state, TeamResource) {

    $scope.users = TeamResource.getUsersInTeam();

});