using FoodNinja.Core.Domain;
using FoodNinja.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace FoodNinja.API.Infrastructure
{
    public class BaseApiController : ApiController
    {
        protected readonly INinjaUserRepository _ninjaUserRepository;

        public BaseApiController(INinjaUserRepository ninjaUserRepository)
        {
            _ninjaUserRepository = ninjaUserRepository;
        }

        protected NinjaUser CurrentUser
        {
            get
            {
                return _ninjaUserRepository.GetFirstOrDefault(nu => nu.UserName == User.Identity.Name);
            }
        }
    }
}