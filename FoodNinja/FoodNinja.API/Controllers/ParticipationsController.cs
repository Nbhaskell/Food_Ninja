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
    public class ParticipationsController : BaseApiController
    {
        private readonly IParticipationRepository _participationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ParticipationsController(IParticipationRepository participationRepository, IUnitOfWork unitOfWork, INinjaUserRepository ninjaUserRepository) : base(ninjaUserRepository)
        {
            _participationRepository = participationRepository;
            _unitOfWork = unitOfWork;
        }

        //GET: api/Participations
        public IEnumerable<ParticipationModel> GetParticipations()
        {
            return Mapper.Map<IEnumerable<ParticipationModel>>(_participationRepository.GetWhere(p => p.NinjaUser.TeamId == CurrentUser.TeamId));
        }

        //GET: api/Participations/5
        [ResponseType(typeof(ParticipationModel))]
        public IHttpActionResult GetParticipation(int id)
        {
            Core.Domain.Participation participation = _participationRepository.GetById(id);

            if (participation == null || participation.NinjaUser.TeamId != CurrentUser.TeamId)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<ParticipationModel>(participation));
        }

        //PUT: api/Participations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutParticipation(int id, ParticipationModel participation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != participation.ParticipationId)
            {
                return BadRequest();
            }

            var dbParticipation = _participationRepository.GetById(id);

            if (dbParticipation == null) return NotFound();

            dbParticipation.Update(participation);

            _participationRepository.Update(dbParticipation);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!ParticipationExists(id))
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

        //POST: api/Participations
        [ResponseType(typeof(ParticipationModel))]
        public IHttpActionResult PostParticipation(ParticipationModel participation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbParticipation = new Core.Domain.Participation(participation);

            _participationRepository.Add(dbParticipation);
            _unitOfWork.Commit();

            return CreatedAtRoute("DefaultApi", new { id = participation.ParticipationId }, participation);
        }

        //DELETE: api/Participations
        [ResponseType(typeof(ParticipationModel))]
        public IHttpActionResult DeleteParticipation(int id)
        {
            Core.Domain.Participation participation = _participationRepository.GetById(id);

            if (participation == null)
            {
                return NotFound();
            }

            _participationRepository.Delete(participation);

            _unitOfWork.Commit();

            return Ok(Mapper.Map<ParticipationModel>(participation));
        }

        private bool ParticipationExists(int id)
        {
            return _participationRepository.Any(p => p.ParticipationId == id);
        }
    }
}