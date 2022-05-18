using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ThedoorCode.Models;

namespace ThedoorCode.Data
{
    public class UserDbContext : IdentityDbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }

        public virtual DbSet<UserModel> UserModels { get; set; }

        public virtual DbSet<Experience> Experiences { get; set; }
        public virtual DbSet<ImageModel> ImageModels { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
