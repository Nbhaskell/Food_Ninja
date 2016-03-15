﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodNinja.Core.Domain
{
    public class Order
    {
        public int OrderId { get; set; }
        public int TeamId { get; set; }
        public int NinjaUserId { get; set; }
        public string OrderName { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Team Team { get; set; }

        public virtual ICollection<Participation> Participations { get; set; }
        public virtual ICollection<RestaurantOption> RestaurantOptions { get; set; }
    }
}