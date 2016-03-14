using FoodNinja.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodNinja.Core.Domain
{
    public class NinjaUser : IUser<string>
    {
        public NinjaUser()
        {

        }
        }
        public NinjaUser(NinjaUsersModel ninjaUser)
        {
            this.Update(ninjaUser);
        }

        public void Update(NinjaUsersModel ninjaUser)
        {
            GroupId = ninjaUser.GroupId;
            UserName = ninjaUser.UserName;
            FirstName = ninjaUser.FirstName;
            LastName = ninjaUser.LastName;
            EmailAddress = ninjaUser.EmailAddress;
            PasswordHash = ninjaUser.PasswordHash;
            SecurityStamp = ninjaUser.SecurityStamp;
            CreatedDate = ninjaUser.CreatedDate;
        }
            public string NinjaUserId { get; set; }
            public string GroupId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string EmailAddress { get; set; }
            public string PasswordHash { get; set; }
            public string SecurityStamp { get; set; }
            public DateTime CreatedDate { get; set; }

            public virtual ICollection<UserRole> UserRoles { get; set; }
            public virtual ICollection<Participation> Participations { get; set; }
            public virtual Group Group { get; set; }
        }
}