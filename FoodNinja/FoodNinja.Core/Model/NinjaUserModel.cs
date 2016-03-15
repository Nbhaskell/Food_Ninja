using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodNinja.Core.Model
{
    public class NinjaUserModel
    {
        public int NinjaUserId { get; set; }
        public int TeamId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public DateTime CreatedDate { get; set; }

        public IEnumerable<ParticipationModel> Participation { get; set; }
    }
}