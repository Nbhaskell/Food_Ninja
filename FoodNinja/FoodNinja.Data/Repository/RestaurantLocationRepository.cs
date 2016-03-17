using FoodNinja.Core.Repository;
using FoodNinja.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using FoodNinja.Core.Domain;

namespace FoodNinja.Data.Repository
{
    public class RestaurantLocationRepository : Repository<RestaurantLocation>, IRestaurantLocationRepository
    {
        public RestaurantLocationRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }
    }
}