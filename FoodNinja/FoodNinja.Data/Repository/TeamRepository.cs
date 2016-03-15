using FoodNinja.Data.Infrastructure;
using FoodNinja.Core.Repository;
using FoodNinja.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace FoodNinja.Data.Repository
{
    public class GroupRepository : Repository<Group>, ITeamRepository
    {
        public GroupRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }
    }
}