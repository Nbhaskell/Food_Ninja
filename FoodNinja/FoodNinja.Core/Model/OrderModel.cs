using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodNinja.Core.Model
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public int TeamId { get; set; }
        public int NinjaUserId { get; set; }
        public int RestaurantOptionId { get; set; }
        public string OrderName { get; set; }
        public DateTime CreatedDate { get; set; }

        public TeamModel Group { get; set; }

     //   public IEnumerable<ParticipationModel> Participations { get; set; }
        public IEnumerable<RestaurantOptionModel> RestaurantOrders { get; set; }
    }
}