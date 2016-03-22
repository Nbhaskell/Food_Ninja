angular.module('app').factory('OrderResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/order/:orderId', { orderId: '@OrderId' },
        {
            'update': {
                method: 'PUT'
            }
        });
});