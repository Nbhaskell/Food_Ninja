using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodNinja.Core.Model
{
    public class NinjaUsersModel
    {
        public string NinjaUserId { get; set; }
        public string GroupId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public DateTime CreatedDate { get; set; }

        public IEnumerable<ParticipationsModel> Participation { get; set; }
    }
}