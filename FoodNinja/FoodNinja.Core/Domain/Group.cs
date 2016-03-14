using FoodNinja.Core.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace FoodNinja.Core.Domain
{
    public class Group
    {
        public Group()
        {
            NinjaUsers = new Collection<Participation>();
        }

        public Group(GroupsModel group)
        {
            this.Update(group);
        }

        public void Update(GroupsModel group)
        {
            GroupId = group.GroupId;
            OrderId = group.OrderId;
            NinjaUserId = group.NinjaUserId;

            if(GroupId == 0)
            {
                // IF IT'S NEW 
                foreach(var foodNinjaUser in group.NinjaUsers)
                {
                    var dbNinjaUser = new NinjaUser();
                    dbNinjaUser.Update(ninjaUser);
                    NinjaUsers.Add(dbNinjaUser);
                }
            }
            else
            {
                // if it exists
                foreach (var modelNinjaUser in group.NinjaUsers)
                {
                    var databaseNinjaUser = NinjaUsers.FirstOrDefault(nu => nu.NinjaUserId == modelNinjaUser.NinjaUserId);

                    databaseNinjaUser.Update(modelNinjaUser);
                }
            }
        }

        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public int Telephone { get; set; }
        public int OrderId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Invite> Invites { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<NinjaUser> NinjaUsers { get; set; }
    }
}