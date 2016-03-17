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
    public class RestaurantOptionsController : BaseApiController
    {
        private readonly IRestaurantOptionRepository _restaurantOptionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RestaurantOptionsController(IRestaurantOptionRepository restaurantOptionRepository, IUnitOfWork unitOfWork, INinjaUserRepository ninjaUserRepository) : base(ninjaUserRepository)
        {
            _restaurantOptionRepository = restaurantOptionRepository;
            _unitOfWork = unitOfWork;
        }

        //GET: api/RestaurantOptions
        //public IQueryable<RestaurantOptionModel> GetRestaurantOptions()
        //{
        //    return _restaurantOptionRepository.GetAll().ProjectTo<RestaurantOptionModel>();
        //}
        [ResponseType(typeof(RestaurantOptionModel))]
        public IEnumerable<RestaurantOptionModel> GetRestaurants()
        {
            return Mapper.Map<IEnumerable<RestaurantOptionModel>>(_restaurantOptionRepository.GetAll());
        }

        //GET: api/RestaurantOptions/5
        [ResponseType(typeof(RestaurantOptionModel))]
        public IHttpActionResult GetRestaurantOption(int id)
        {
            Core.Domain.RestaurantOption restaurantOption = _restaurantOptionRepository.GetById(id);

            if (restaurantOption == null || restaurantOption.Order.TeamId != CurrentUser.TeamId)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<RestaurantOptionModel>(restaurantOption));
        }
        


        private bool RestaurantOptionExists(int restaurantLocationId, int orderId)
        {
            return _restaurantOptionRepository.Any(ro => ro.RestaurantLocationId == restaurantLocationId && 
                                                         ro.OrderId == orderId);
        }

    }
}
