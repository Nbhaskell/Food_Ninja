using AutoMapper;
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
    public class RestaurantOptionsController : ApiController
    {
        private readonly IRestaurantOptionRepository _restaurantOptionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RestaurantOptionsController(IRestaurantOptionRepository restaurantOptionRepository, IUnitOfWork unitOfWork)
        {
            _restaurantOptionRepository = restaurantOptionRepository;
            _unitOfWork = unitOfWork;
        }

        //GET: api/RestaurantOptions
        public IQueryable<RestaurantOptionModel> GetRestaurantOptions()
        {
            return _restaurantOptionRepository.GetAll().ProjectTo<RestaurantOptionModel>();
        }

        //GET: api/RestaurantOptions/5
        [ResponseType(typeof(RestaurantOptionModel))]
        public IHttpActionResult GetRestaurantOption(int id)
        {
            Core.Domain.RestaurantOption restaurantOption = _restaurantOptionRepository.GetById(id);

            if (restaurantOption == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<RestaurantOptionModel>(restaurantOption));
        }

        //PUT: api/RestaurantOptions
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRestaurantOption(int id, RestaurantOptionModel restaurantOption)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != restaurantOption.RestaurantOptionId)
            {
                return BadRequest();
            }

            var dbRestaurantOption = _restaurantOptionRepository.GetById(id);

            if (dbRestaurantOption == null) return NotFound();

            dbRestaurantOption.Update(restaurantOption);

            _restaurantOptionRepository.Update(dbRestaurantOption);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!RestaurantOptionExists(id))
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


        private bool RestaurantOptionExists(int id)
        {
            return _restaurantOptionRepository.Any(ro => ro.RestaurantOptionId == id);
        }

    }
}
