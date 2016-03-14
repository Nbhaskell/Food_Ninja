using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodNinja.Core.Model
{
    public class RestaurantLocationsModel
    {
        public string RestaurantLocationId { get; set; }
        public string RestaurantId { get; set; }
        public DateTime CreatedDate { get; set; }

        public IEnumerable<RestaurantOrdersModel> RestaurantOrders { get; set; }
    }
}