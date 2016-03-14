using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodNinja.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        NinjaDataContext GetDataContext();
    }
}