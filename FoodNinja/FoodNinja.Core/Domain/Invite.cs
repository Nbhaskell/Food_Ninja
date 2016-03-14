using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodNinja.Core.Domain
{
    public class Invite
    {
        public string InviteId { get; set; }
        public string GroupId { get; set; }
        public string Token { get; set; }

        public virtual Group Group { get; set; }
    }
}