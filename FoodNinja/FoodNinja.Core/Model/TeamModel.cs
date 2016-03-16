using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodNinja.Core.Model
{
    public class TeamModel
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string Telephone { get; set; }
        public DateTime CreatedDate { get; set; }

        public IEnumerable<NinjaUserModel> NinjaUsers { get; set; }
        public IEnumerable<OrderModel> Orders { get; set; }
        public IEnumerable<InviteModel> Invites { get; set; }
    }
}