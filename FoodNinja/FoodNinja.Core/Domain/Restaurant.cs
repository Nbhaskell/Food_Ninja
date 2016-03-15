using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodNinja.Core.Domain
{
    public class Restaurant
    {
        public int RestaurantId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }

        public virtual ICollection<RestaurantLocation> RestaurantLocations { get; set; }
    }
}