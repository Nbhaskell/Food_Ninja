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
    
    public class RestaurantLocationsController : BaseApiController
    {
        private readonly IRestaurantLocationRepository _restaurantLocationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RestaurantLocationsController(IRestaurantLocationRepository restaurantLocationRepository, IUnitOfWork unitOfWork, INinjaUserRepository ninjaUserRepository) : base(ninjaUserRepository)
        {
            _restaurantLocationRepository = restaurantLocationRepository;
            _unitOfWork = unitOfWork;
        }

        //GET: api/RestaurantLocations
        public IEnumerable<RestaurantLocationModel> GetRestaurantLocations()
        {
            return _restaurantLocationRepository.GetAll();
        }
        
        //GET: api/RestaurantLocations/5
        [ResponseType(typeof(RestaurantLocationModel))]
        public IHttpActionResult GetRestaurantLocation(int id)
        {
            Core.Domain.RestaurantLocation restaurantLocation = _restaurantLocationRepository.GetById(id);

            if (restaurantLocation == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<RestaurantLocationModel>(restaurantLocation));
        }

        //PUT: api/RestaurantLocations
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRestaurantLocation(int id, RestaurantLocationModel restaurantLocation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != restaurantLocation.RestaurantLocationId)
            {
                return BadRequest();
            }

            var dbRestaurantLocation = _restaurantLocationRepository.GetById(id);

            if (dbRestaurantLocation == null) return NotFound();

            dbRestaurantLocation.Update(restaurantLocation);

            _restaurantLocationRepository.Update(dbRestaurantLocation);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!RestaurantLocationExists(id))
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




        //POST: api/RestaurantLocations
        [ResponseType(typeof(RestaurantLocationModel))]
        public IHttpActionResult PostRestaurantLocation(RestaurantLocationModel restaurantLocation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbRestaurantLocation = new Core.Domain.RestaurantLocation(restaurantLocation);

            _restaurantLocationRepository.Add(dbRestaurantLocation);
            _unitOfWork.Commit();

            return CreatedAtRoute("DefaultApi", new { id = restaurantLocation.RestaurantLocationId }, restaurantLocation);
        }
        
        //DELETE: api/RestaurantLocations/5
        [ResponseType(typeof(RestaurantLocationModel))]
        public IHttpActionResult DeleteRestaurantLocation(int id)
        {
            Core.Domain.RestaurantLocation restaurantLocation = _restaurantLocationRepository.GetById(id);

            if (restaurantLocation == null)
            {
                return NotFound();
            }

            _restaurantLocationRepository.Delete(restaurantLocation);

            _unitOfWork.Commit();

            return Ok(Mapper.Map<RestaurantLocationModel>(restaurantLocation));
        }

        private bool RestaurantLocationExists(int id)
        {
            return _restaurantLocationRepository.Any(rl => rl.RestaurantLocationId == id);
        }
    }
}
