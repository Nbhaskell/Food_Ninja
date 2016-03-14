using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodNinja.Core.Model
{
    public class GroupsModel
    {
        public string GroupName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public int Telephone { get; set; }
        public int GroupId { get; set; }
        public DateTime CreatedDate { get; set; }

        public IEnumerable<NinjaUsersModel> NinjaUsers { get; set; }
        public IEnumerable<OrdersModel> Orders { get; set; }
    }
}