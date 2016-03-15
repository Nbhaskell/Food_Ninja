using FoodNinja.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodNinja.Core.Domain
{
    public class RestaurantOption
    {

        public RestaurantOption()
        {
            
        }

        public RestaurantOption(RestaurantOptionModel restaurantOption) : this()
        {
            Update(restaurantOption);
            CreatedDate = DateTime.Now;
        }

        public int OrderId { get; set; }
        public int RestaurantLocationId { get; set; }
        public DateTime CreatedDate { get; set; }
        
        public virtual Order Order { get; set; }
        public virtual RestaurantLocation RestaurantLocation { get; set; }
        public virtual ICollection<Participation> Participations { get; set; }

        public void Update (RestaurantOptionModel restaurantOption)
        {

        }
    }
}