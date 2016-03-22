angular.module('app').factory('ParticipationResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/participation/:participationId', { participationId: '@ParticipationId' },
        {
            'update': {
                method: 'PUT'
            }
        });
});