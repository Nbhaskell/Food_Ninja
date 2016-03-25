using AutoMapper;
using FoodNinja.API.Infrastructure;
using FoodNinja.API.Requests;
using FoodNinja.Core.Domain;
using FoodNinja.Core.Infrastructure;
using FoodNinja.Core.Model;
using FoodNinja.Core.Repository;
using FoodNinja.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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
        [AllowAnonymous]
        [ResponseType(typeof(InviteModel))]
        [Route("api/invite/{token}")]
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
        [Authorize(Roles = "Admin")]
        [Route("api/invite/new")]
        public IHttpActionResult CreateInvite(InviteUserRequest request)
        {
            var invite = new Invite
            {
                 TeamId = CurrentUser.TeamId,
                 Token = Security.GetTimestampedToken()
            };

            _inviteRepository.Add(invite);

            _unitOfWork.Commit();

            var sb = new StringBuilder();

            sb.AppendLine("Hello!");
            sb.AppendLine();
            sb.AppendLine($"{CurrentUser.FirstName} has invited you to their Food Ninja team, click the link below to get started.");
            sb.AppendLine();
            sb.AppendLine($"http://foodninja.azurewebsites.net/#/inviteRegister/{invite.Token}");
            sb.AppendLine();
            sb.AppendLine("Please do not reply to this email");
            
            Mailbox.SendEmail(request.Email, "Food Ninja <noreply@foodninja.com>", $"You have been invited to join the {CurrentUser.Team.TeamName} team on Food Ninja!", sb.ToString());

            return Ok();
        }

        //DELETE n/a

    }
}
