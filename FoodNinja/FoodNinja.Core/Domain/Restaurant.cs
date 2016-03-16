using FoodNinja.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodNinja.Core.Domain
{
    public class Restaurant
    {
        public Restaurant()
        {

        }

        public Restaurant(RestaurantModel restaurant) : this()
        {
            Update(restaurant);
            CreatedDate = DateTime.Now;
            Name = restaurant.Name;
            Description = restaurant.Description;
            URL = restaurant.URL;
        }

        public int RestaurantId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }

        public virtual ICollection<RestaurantLocation> RestaurantLocations { get; set; }

        public void Update(RestaurantModel restaurant)
        {

        }
    }
}