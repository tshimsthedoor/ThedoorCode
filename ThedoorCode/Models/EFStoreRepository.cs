using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThedoorCode.Data;

namespace ThedoorCode.Models
{
    public class EFStoreRepository : IStoreRepository
    {
        private StoreDbContext _context;

        public EFStoreRepository(StoreDbContext context)
        {
            _context = context;
        }
        public IQueryable<Product> Products => _context.Products;
    }
}
