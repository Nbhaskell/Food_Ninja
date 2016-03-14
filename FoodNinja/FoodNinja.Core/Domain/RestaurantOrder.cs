using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodNinja.Core.Domain
{
    public class RestaurantOrder
    {
        public string RestaurantOrderId { get; set; } 
        public string OrderId { get; set; }
        public int CreatedDate { get; set; }
        
        public virtual Order Order { get; set; }
        public virtual RestaurantLocation RestaurantLocation { get; set; }
    }
}