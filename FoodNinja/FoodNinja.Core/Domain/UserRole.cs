using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodNinja.Core.Domain
{
    public class UserRole
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public virtual NinjaUser NinjaUser { get; set; }
        public virtual Role Role { get; set; }
    }
}