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
    public class ParticipationRepository : Repository<Participation>, IParticipationRepository
    {
        public ParticipationRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }
    }
}