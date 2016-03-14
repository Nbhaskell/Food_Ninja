using Microsoft.AspNet.Identity;
using FoodNinja.Core.Domain;
using FoodNinja.Core.Infrastructure;
using FoodNinja.Core.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodNinja.Data.Infrastructure
{
    public class AuthorizationRepository
    {
        private readonly IUserStore<NinjaUser> _userStore;
        private readonly IDatabaseFactory _databaseFactory;
        private readonly UserManager<NinjaUser> _userManager;

        private NinjaDataContext db;
        protected NinjaDataContext Db => db ?? (db = _databaseFactory.GetDataContext());

        public AuthorizationRepository(IDatabaseFactory databaseFactory, IUserStore<NinjaUser> userStore)
        {
            _userStore = userStore;
            _databaseFactory = databaseFactory;
            _userManager = new UserManager<NinjaUser>(userStore);
        }

        public async Task<IdentityResult> RegisterUser(RegistrationModel model)
        {
            //create a user

            var ninjaUser = new NinjaUser
            {
                UserName = model.EmailAddress,
                Email = model.EmailAddress,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PasswordHash = model.PasswordHash,
                SecurityStamp = model.SecurityStamp,
                CreatedDate = model.CreatedDate
            };

            //Save the user

            var result = await _userManager.CreateAsync(ninjaUser, model.Password);

            await _userManager.AddToRoleAsync(ninjaUser.Id, "User");

            return result;
        }

        // Assign admin role

        public async Task<IdentityResult> RegisterAdmin(RegistrationModel model)
        {
            // create a user
            var ninjaUser = new NinjaUser
            {
                UserName = model.EmailAddress,
                EmailTokenProvider = model.EmailAddress
            };

            //save the user
            var result = await _userManager.CreateAsync(ninjaUser, model.Password);

            await _userManager.AddToRoleAsync(ninjaUser.Id, "Admin");

            return result;
        }

        public async Task<NinjaUser> FindUser(string username, string password)
        {
            return await _userManager.FindAsync(username, password);
        }
    }
}   