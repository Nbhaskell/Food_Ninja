angular.module('app').factory('AccountResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/NinjaUser/:NinjaUserId', { NinjaUserId: '@NinjaUserId' },
    {
        'update': {
            method: 'PUT'
        }
    });
});