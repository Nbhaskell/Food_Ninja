angular.module('app').factory('RestaurantOptionResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/restaurantoption/:restaurantOptionId', { restaurantOptionId: '@RestaurantOptionId' },
        {
            'update': {
                method: 'PUT'
            }
        });
});