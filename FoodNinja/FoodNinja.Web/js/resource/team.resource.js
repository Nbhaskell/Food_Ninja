angular.module('app').factory('TeamResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/team/:teamId', { teamId: '@TeamId' },
        {
            'update': {
                method: 'PUT'
            }
        });
});