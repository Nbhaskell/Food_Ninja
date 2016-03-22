angular.module('app').factory('RestaurantResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/restaurant/:restaurantId', { restaurantId: '@RestaurantId' },
        {
            'update': {
                method: 'PUT'
            }
        });
});