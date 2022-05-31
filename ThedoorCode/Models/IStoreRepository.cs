using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThedoorCode.Models
{
    public interface IStoreRepository
    {
        IQueryable<Product> Products { get; }
    }
}
