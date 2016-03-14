using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodNinja.Core.Domain
{
    public class Restaurant
    {
        public string RestaurantId { get; set; }
        public int CreatedDate { get; set; }

        public virtual ICollection<RestaurantLocation> RestaurantLocations { get; set; }
    }
}