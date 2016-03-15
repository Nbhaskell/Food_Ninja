using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodNinja.Core.Domain
{
    public class Invite
    {
        public int InviteId { get; set; }
        public int TeamId { get; set; }
        public string Token { get; set; }

        public virtual Team Team { get; set; }
    }
}