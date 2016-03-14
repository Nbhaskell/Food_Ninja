using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodNinja.Core.Model
{
    public class RestaurantsModel
    {
        public string RestaurantId { get; set; }
        public int CreatedDate { get; set; }

        public IEnumerable<RestaurantLocationsModel> RestaurantLocations { get; set; }
    }
}