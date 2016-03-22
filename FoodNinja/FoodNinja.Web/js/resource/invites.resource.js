angular.module('app').factory('InvitesResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/invites/:inviteId', { inviteId: '@InviteId' },
        {
            'update': {
                method: 'PUT'
            }
        });
});