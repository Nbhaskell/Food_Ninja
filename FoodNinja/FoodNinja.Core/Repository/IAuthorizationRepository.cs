using FoodNinja.Core.Domain;
using FoodNinja.Core.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FoodNinja.Core.Repository
{
    public interface IAuthorizationRepository
    {
        Task<NinjaUser> FindUser(string username, string password);
        Task<IdentityResult> RegisterAdmin(RegistrationModel model);
        Task<IdentityResult> RegisterUser(RegistrationModel model);
    }
}