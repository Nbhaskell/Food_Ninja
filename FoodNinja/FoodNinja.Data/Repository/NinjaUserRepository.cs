using FoodNinja.Core.Domain;
using FoodNinja.Core.Repository;
using FoodNinja.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace FoodNinja.Data.Repository
{
    public class NinjaUserRepository : Repository<NinjaUser>, INinjaUserRepository
    {
        public NinjaUserRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }
    }
}