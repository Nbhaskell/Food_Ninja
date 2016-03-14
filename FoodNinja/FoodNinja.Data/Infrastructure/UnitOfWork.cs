using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodNinja.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly IDatabaseFactory _databaseFactory;
        private NinjaDataContext _dataContext;

        protected NinjaDataContext DataContext
        {
            get
            {
                return _dataContext ?? (_dataContext = _databaseFactory.GetDataContext());
            }
        }

        public void Commit()
        {
            DataContext.SaveChanges();
        }

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }
    }
}