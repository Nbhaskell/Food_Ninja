﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public class NinjaUsersController : ApiController
    {
        private INinjaUserRepository _ninjaUserRepository;
        private IUnitOfWork _unitOfWork;

        public NinjaUsersController(INinjaUserRepository ninjaUserRepository, IUnitOfWork unitOfWork)
        {
            _ninjaUserRepository = ninjaUserRepository;
            _unitOfWork = unitOfWork;
        }

        //GET: api/NinjaUsers
        public IQueryable<NinjaUserModel> GetNinjaUsers()
        {
            return _ninjaUserRepository.GetAll().ProjectTo<NinjaUserModel>();
        }

        //GET: api/NinjaUsers/5
        [ResponseType(typeof(NinjaUserModel))]
        public IHttpActionResult GetNinjaUser(int id)
        {
            Core.Domain.NinjaUser ninjaUser = _ninjaUserRepository.GetById(id);

            if (ninjaUser == null)
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

            if (dbNinjaUser == null) return NotFound();

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

        //POST: api/NinjaUsers/5
        [ResponseType(typeof(NinjaUserModel))]
        public IHttpActionResult PostNinjaUser(NinjaUserModel ninjaUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbNinjaUser = new Core.Domain.NinjaUser(ninjaUser);

            _ninjaUserRepository.Add(dbNinjaUser);
            _unitOfWork.Commit();

            return CreatedAtRoute("DefaultApi", new { id = ninjaUser.NinjaUserId }, ninjaUser);
        }

        //DELETE: api/NinjaUsers/5
        [ResponseType(typeof(NinjaUserModel))]
        public IHttpActionResult DeleteNinjaUser(int id)
        {
            Core.Domain.NinjaUser ninjaUser = _ninjaUserRepository.GetById(id);

            if (ninjaUser == null)
            {
                return NotFound();
            }

            _ninjaUserRepository.Delete(ninjaUser);

            _unitOfWork.Commit();

            return Ok(Mapper.Map<NinjaUserModel>(ninjaUser));
        }

        private bool TeamExists(int id)
        {
            return _ninjaUserRepository.Any(nu => nu.NinjaUserId == id);
        }
    }
}
