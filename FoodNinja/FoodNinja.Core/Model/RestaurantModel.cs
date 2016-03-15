using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodNinja.Core.Model
{
    public class RestaurantModel
    {
        public int RestaurantId { get; set; }
        public DateTime CreatedDate { get; set; }

        public IEnumerable<RestaurantLocationModel> RestaurantLocations { get; set; }
    }
}