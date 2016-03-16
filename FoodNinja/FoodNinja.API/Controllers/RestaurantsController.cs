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
    public class RestaurantsController : ApiController
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RestaurantsController(IRestaurantRepository restaurantRepository, IUnitOfWork unitOfWork)
        {
            _restaurantRepository = restaurantRepository;
            _unitOfWork = unitOfWork;
        }

        //GET: api/Restaurants
        public IQueryable<RestaurantModel> GetRestaurants()
        {
            return _restaurantRepository.GetAll().ProjectTo<RestaurantModel>();
        }

        //GET: api/Restaurants/5
        [ResponseType(typeof(RestaurantModel))]
        public IHttpActionResult GetRestaurant(int id)
        {
            Core.Domain.Restaurant restaurant = _restaurantRepository.GetById(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<RestaurantModel>(restaurant));
        }

        //PUT: api/Restaurants/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRestauant(int id, RestaurantModel restaurant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != restaurant.RestaurantId)
            {
                return BadRequest();
            }

            var dbRestaurant = _restaurantRepository.GetById(id);

            if (dbRestaurant == null) return NotFound();

            dbRestaurant.Update(restaurant);

            _restaurantRepository.Update(dbRestaurant);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!RestaurantExists(id))
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

        //POST: api/Restaurants
        [ResponseType(typeof(RestaurantModel))]
        public IHttpActionResult PostRestaurant(RestaurantModel restaurant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbRestaurant = new Core.Domain.Restaurant(restaurant);

            _restaurantRepository.Add(dbRestaurant);
            _unitOfWork.Commit();

            return CreatedAtRoute("DefaultApi", new { id = restaurant.RestaurantId }, restaurant);
        }

        //DELETE: api/Restaurants/5
        [ResponseType(typeof(RestaurantModel))]
        public IHttpActionResult DeleteRestaurant(int id)
        {
            Core.Domain.Restaurant restaurant = _restaurantRepository.GetById(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            _restaurantRepository.Delete(restaurant);

            _unitOfWork.Commit();

            return Ok(Mapper.Map<RestaurantModel>(restaurant));
        }

        private bool RestaurantExists(int id)
        {
            return _restaurantRepository.Any(r => r.RestaurantId == id);
        }

    }
}
