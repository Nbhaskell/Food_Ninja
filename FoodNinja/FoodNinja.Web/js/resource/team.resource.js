angular.module('app').factory('TeamResource', function (apiUrl, $resource, $http) {
    return $resource(apiUrl + '/team/:teamId', { teamId: '@TeamId' },
    {
        'update': {
            method: 'PUT'
        },
        'getUsersInTeam': {
            method: 'GET',
            isArray: true,
            url: apiUrl + '/teams/users'
        }
    });
});