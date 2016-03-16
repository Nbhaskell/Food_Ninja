using FoodNinja.Core.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodNinja.Core.Domain
{
    public class NinjaUser : IUser<int>
    {
        public NinjaUser()
        {

        }

        public NinjaUser(NinjaUserModel ninjaUser)
        {
            this.Update(ninjaUser);
        }

        public void Update(NinjaUserModel ninjaUser)
        {
            TeamId = ninjaUser.TeamId;
            UserName = ninjaUser.UserName;
            FirstName = ninjaUser.FirstName;
            LastName = ninjaUser.LastName;
            EmailAddress = ninjaUser.EmailAddress;
            PasswordHash = ninjaUser.PasswordHash;
            SecurityStamp = ninjaUser.SecurityStamp;
            CreatedDate = ninjaUser.CreatedDate;
        }
        public int NinjaUserId { get; set; }
        public int TeamId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<Participation> Participations { get; set; }
        public virtual Team Team { get; set; }
    }
}