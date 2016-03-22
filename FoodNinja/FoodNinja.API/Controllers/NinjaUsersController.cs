using AutoMapper;
using AutoMapper.QueryableExtensions;
using FoodNinja.API.Infrastructure;
using FoodNinja.Core.Infrastructure;
using FoodNinja.Core.Model;
using FoodNinja.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace FoodNinja.API.Controllers
{
    [Authorize]
    public class NinjaUsersController : BaseApiController
    {
        private IUnitOfWork _unitOfWork;

        public NinjaUsersController(INinjaUserRepository ninjaUserRepository, IUnitOfWork unitOfWork) : base(ninjaUserRepository)
        {
            _unitOfWork = unitOfWork;
        }

        //GET: api/NinjaUsers
        public IEnumerable<NinjaUserModel> GetNinjaUsers()
        {
            return Mapper.Map<IEnumerable<NinjaUserModel>>(_ninjaUserRepository.GetWhere(nu => nu.TeamId == CurrentUser.TeamId));
        }

        //GET: api/NinjaUsers/5
        [ResponseType(typeof(NinjaUserModel))]
        public IHttpActionResult GetNinjaUser(int id)
        {
            Core.Domain.NinjaUser ninjaUser = _ninjaUserRepository.GetById(id);            

            if (ninjaUser == null || ninjaUser.TeamId != CurrentUser.TeamId)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<NinjaUserModel>(ninjaUser));
        }

        //PUT: api/NinjaUsers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNinjaUser(int id, NinjaUserModel ninjaUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ninjaUser.NinjaUserId)
            {
                return BadRequest();
            }

            var dbNinjaUser = _ninjaUserRepository.GetById(id);

            if (dbNinjaUser == null || dbNinjaUser.Id != CurrentUser.Id) return NotFound();

            dbNinjaUser.Update(ninjaUser);

            _ninjaUserRepository.Update(dbNinjaUser);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!TeamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        //POST: n/a


        //DELETE: api/NinjaUsers/5
        [Authorize (Roles = "Admin")]
        [ResponseType(typeof(NinjaUserModel))]
        public IHttpActionResult DeleteNinjaUser(int id)
        {
            Core.Domain.NinjaUser ninjaUser = _ninjaUserRepository.GetById(id);

            if (ninjaUser == null || ninjaUser.TeamId != CurrentUser.TeamId)
            {
                return NotFound();
            }

            _ninjaUserRepository.Delete(ninjaUser);

            _unitOfWork.Commit();

            return Ok(Mapper.Map<NinjaUserModel>(ninjaUser));
        }

        private bool TeamExists(int id)
        {
            return _ninjaUserRepository.Any(nu => nu.Id == id);
        }
    }
}
