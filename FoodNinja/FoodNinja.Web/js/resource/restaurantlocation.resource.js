angular.module('app').factory('RestaurantLocationResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/restaurantlocation/:restaurantLocationId', { restaurantLocationId: '@RestaurantLocationId' },
        {
            'update': {
                method: 'PUT'
            }
        });
});