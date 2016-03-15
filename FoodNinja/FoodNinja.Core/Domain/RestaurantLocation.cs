using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodNinja.Core.Domain
{
    public class RestaurantLocation
    {
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
    }
}