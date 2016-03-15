using FoodNinja.Core.Model;
using FoodNinja.Core.Repository;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FoodNinja.API.Controllers
{
    public class AccountsController : ApiController
    {
        private IAuthorizationRepository _repo;

        public AccountsController(IAuthorizationRepository repo)
        {
            _repo = repo;
        }

        // POST API/Account/Register

        [AllowAnonymous]
        [Route("api/accounts/register/users")]
        public async Task<IHttpActionResult> RegisterUser(RegistrationModel registration)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _repo.RegisterUser(registration);

            if(result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Registration form was invalid.");
            }
        }

        [AllowAnonymous]
        [Route("api/accounts/register/admin")]
        public async Task<IHttpActionResult> RegisterAdmin(RegistrationModel registration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _repo.RegisterAdmin(registration);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Registration form was invalid.");
            }
        }

    }
}
