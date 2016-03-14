using FoodNinja.Core.Domain;
using FoodNinja.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace FoodNinja.Core.Repository
{
    public interface IRestaurantRepository : IRepository<Restaurant>
    {
    }
}