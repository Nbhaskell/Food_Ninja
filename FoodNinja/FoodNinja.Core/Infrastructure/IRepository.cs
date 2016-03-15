using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace FoodNinja.Core.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class
    {
        // Create
        TEntity Add(TEntity entity);

        // Read
       
        TEntity GetById(object id);
        IQueryable<TEntity> GetAll();
        IEnumerable<TEntity> GetWhere(Expression<Func<TEntity, bool>> where);
        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> where);
        int Count();
        int Count(Func<TEntity, bool> where);
        bool Any(Expression<Func<TEntity, bool>> condition);

        // Update
        void Update(TEntity entity);

        // Delete
        void Delete(TEntity entity);
    }
}