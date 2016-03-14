using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodNinja.Core.Domain
{
    public class RestaurantLocation
    {
        public string RestaurantLocationId { get; set; }
        public string RestaurantId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        public virtual ICollection<RestaurantOrder> RestaurantOrders { get; set; }
    }
}