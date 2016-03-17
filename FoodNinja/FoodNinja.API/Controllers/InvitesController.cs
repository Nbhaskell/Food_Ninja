using AutoMapper;
using FoodNinja.API.Infrastructure;
using FoodNinja.Core.Domain;
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
    public class InvitesController : BaseApiController
    {
        private IInviteRepository _inviteRepository;
        private IUnitOfWork _unitOfWork;
        private ITeamRepository _teamRepository;

        public InvitesController(IInviteRepository inviteRepository, IUnitOfWork unitOfWork, INinjaUserRepository ninjaUserRepository, ITeamRepository teamRepository) : base(ninjaUserRepository)
        {
            _inviteRepository = inviteRepository;
            _unitOfWork = unitOfWork;
            _teamRepository = teamRepository;
        }

        // GET api/Invites
        [ResponseType(typeof(InviteModel))]
        public IHttpActionResult GetInvite(string token)
        {     
            Invite dbInvite = _inviteRepository.GetFirstOrDefault(i => i.Token == token);
            if (dbInvite == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<InviteModel>(dbInvite));
        }

        //PUT n/a

        //POST n/a

        //DELETE n/a

    }
}
