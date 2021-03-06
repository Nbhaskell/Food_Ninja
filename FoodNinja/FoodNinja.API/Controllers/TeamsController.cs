﻿using AutoMapper;
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
    public class TeamsController : BaseApiController
    {
        private ITeamRepository _teamRepository;
        private IUnitOfWork _unitOfWork;

        public TeamsController(ITeamRepository teamRepository, IUnitOfWork unitOfWork, INinjaUserRepository ninjaUserRepository) : base(ninjaUserRepository)
        {
            _teamRepository = teamRepository;
            _unitOfWork = unitOfWork;
        }

        //GET: api/Teams/5
        [ResponseType(typeof(TeamModel))]
        public IHttpActionResult GetTeam(int id)
        {
            return Ok(Mapper.Map<TeamModel>(CurrentUser.Team));
        }

        //GET: api/Teams/Users
        [Route("api/teams/users")]
        public IEnumerable<NinjaUserModel> GetUsersForTeam()
        {
            return Mapper.Map<IEnumerable<NinjaUserModel>>(CurrentUser.Team.NinjaUsers);
        }


        //PUT: api/Teams/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTeam(int id, TeamModel team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != team.TeamId)
            {
                return BadRequest();
            }

            var dbTeam = _teamRepository.GetById(id);

            if (dbTeam == null) return NotFound();

            dbTeam.Update(team);

            _teamRepository.Update(dbTeam);

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

        private bool TeamExists(int id)
        {
            return _teamRepository.Any(t => t.TeamId == id);
        }
    }
}
