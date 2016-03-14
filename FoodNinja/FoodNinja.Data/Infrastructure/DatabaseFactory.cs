using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodNinja.Core.Infrastructure;
using System.Threading.Tasks;

namespace FoodNinja.Data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private readonly NinjaDataContext _dataContext;

        public NinjaDataContext GetDataContext()
        {
            return _dataContext ?? new NinjaDataContext();
        }

        public DatabaseFactory()
        {
            _dataContext = new NinjaDataContext();
        }

        protected override void DisposeCore()
        {
            if (_dataContext != null) _dataContext.Dispose();
        }
    }
}