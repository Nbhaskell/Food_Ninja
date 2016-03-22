angular.module('app').factory('NinjaUserResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/ninjauser/:ninjaUserId', { ninjaUserId: '@NinjaUserId' },
        {
            'update': {
                method: 'PUT'
            }
        });
});