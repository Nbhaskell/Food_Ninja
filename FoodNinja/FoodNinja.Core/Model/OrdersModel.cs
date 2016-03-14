using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodNinja.Core.Model
{
    public class OrdersModel
    {
        public string OrderId { get; set; }
        public string GroupId { get; set; }
        public string NinjaUserId { get; set; }
        public string RestaurantOrderId { get; set; }
        public string OrderName { get; set; }
        public DateTime CreatedDate { get; set; }

        public GroupsModel Group { get; set; }

        public IEnumerable<ParticipationsModel> Participations { get; set; }
        public IEnumerable<RestaurantOrdersModel> RestaurantOrders { get; set; }
    }
}