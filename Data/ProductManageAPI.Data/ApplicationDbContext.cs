using Microsoft.EntityFrameworkCore;
using ProductManageAPI.Domain;
using System.Collections.Generic;

namespace ProductManageAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ProductEntity> Products { get; set; }
    }
}
