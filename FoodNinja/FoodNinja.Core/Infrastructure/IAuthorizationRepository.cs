using Microsoft.AspNet.Identity;
using FoodNinja.Core.Domain;
using FoodNinja.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawze.Core.Infrastructure
{
    public interface IAuthorizationRepository
    {
        Task<NinjaUser> FindUser(string username, string password);
        Task<IdentityResult> RegisterAdmin(RegistrationModel model);
        Task<IdentityResult> RegisterUser(RegistrationModel model);
    }
}
