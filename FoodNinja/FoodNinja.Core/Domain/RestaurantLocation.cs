using FoodNinja.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodNinja.Core.Domain
{
    public class RestaurantLocation
    {
        public RestaurantLocation()
        {

        }

        public RestaurantLocation(RestaurantLocationModel restaurantLocation) : this()
        {
            Update(restaurantLocation);
            CreatedDate = DateTime.Now;
            Address1 = restaurantLocation.Address1;
            Address2 = restaurantLocation.Address2;
            Address3 = restaurantLocation.Address3;
            City = restaurantLocation.City;
            State = restaurantLocation.State;
            PostCode = restaurantLocation.PostCode;
            Telephone = restaurantLocation.Telephone;
        }

        public int RestaurantLocationId { get; set; }
        public int RestaurantId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string Telephone { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        public virtual ICollection<RestaurantOption> RestaurantOptions { get; set; }

        public void Update(RestaurantLocationModel restaurantLocation)
        {

        }

    }
}