using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodNinja.Core.Domain
{
    public class Role: IRole<string>
    {
        public string RoleId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}