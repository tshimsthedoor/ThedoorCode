using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThedoorCode.Models;

namespace ThedoorCode.Data
{
    public class StoreDbContext : DbContext
    {
     public StoreDbContext(DbContextOptions<StoreDbContext> options)
             : base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}
