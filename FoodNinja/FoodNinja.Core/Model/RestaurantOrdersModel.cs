using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodNinja.Core.Model
{
    public class RestaurantOrdersModel
    {
        public string RestaurantOrderId { get; set; }
        public string OrderId { get; set; }
        public int CreatedDate { get; set; }
    }
}