using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodNinja.Core.Model
{
    public class InviteModel
    {
        public int InviteId { get; set; }
        public int TeamId { get; set; }
        public string Token { get; set; }
    }
}