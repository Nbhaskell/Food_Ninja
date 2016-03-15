using FoodNinja.Core.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace FoodNinja.Core.Domain
{
    public class Team
    {
        public Team()
        {
            Invites = new Collection<Invite>();
            NinjaUsers = new Collection<NinjaUser>();
            Orders = new Collection<Order>();
        }

        public Team(TeamModel team)
        {
            this.Update(team);
        }

        public void Update(TeamModel team)
        {
            TeamId = team.TeamId;
            Name = team.Name;
            Address1 = team.Address1;
            Address2 = team.Address2;
            Address3 = team.Address3;
            City = team.City;
            State = team.State;
            PostCode = team.PostCode;
            Telephone = team.Telephone;
            CreatedDate = team.CreatedDate;
        }

        public int TeamId { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string Telephone { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Invite> Invites { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<NinjaUser> NinjaUsers { get; set; }
    }
}