using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodNinja.Core.Domain
{
    public class Order
    {
        public string OrderId { get; set; }
        public string GroupId { get; set; }
        public string NinjaUserId { get; set; }
        public string RestaurantOrderId { get; set; }
        public string OrderName { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Group Group { get; set; }

        public virtual ICollection<Participation> Paticipations { get; set; }
        public virtual ICollection<RestaurantOrder> RestaurantOrders { get; set; }
    }
}