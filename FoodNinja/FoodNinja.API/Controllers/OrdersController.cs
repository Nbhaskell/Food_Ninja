using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FoodNinja.Core.Infrastructure;
using FoodNinja.Core.Model;
using FoodNinja.Core.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace FoodNinja.API.Controllers
{
    [Authorize]
    public class OrdersController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IUnitOfWork unitOfWork, IOrderRepository orderRepository)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
        }
        //GET: api/Orders
        public IQueryable<OrderModel> GetOrders()
        {
            return _orderRepository.GetAll().ProjectTo<OrderModel>();
        }

        //GET: api/Orders/5
        [ResponseType(typeof(OrderModel))]
        public IHttpActionResult GetOrder(int id)
        {
            Core.Domain.Order order = _orderRepository.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<OrderModel>(order));
        }

        //PUT: api/Orders
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrder(int id, OrderModel order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.OrderId)
            {
                return BadRequest();
            }

            var dbOrder = _orderRepository.GetById(id);

            if (dbOrder == null) return NotFound();

            dbOrder.Update(order);

            _orderRepository.Update(dbOrder);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!OrderExists(id))
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
        //POST: api/Orders
        [ResponseType(typeof(OrderModel))]
        public IHttpActionResult PostOrder(OrderModel order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbOrder = new Core.Domain.Order(order);

            _orderRepository.Add(dbOrder);
            _unitOfWork.Commit();

            return CreatedAtRoute("DefaultApi", new { id = order.OrderId }, order);
        }

        //DELETE: api/Orders
        [ResponseType(typeof(OrderModel))]
        public IHttpActionResult DeleteOrder(int id)
        {
            Core.Domain.Order order = _orderRepository.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            _orderRepository.Delete(order);

            _unitOfWork.Commit();

            return Ok(Mapper.Map<OrderModel>(order));
        }

        private bool OrderExists(int id)
        {
            return _orderRepository.Any(o => o.OrderId == id);
        }

    }
}

