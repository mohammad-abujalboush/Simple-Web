using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using NewSystem_Temp.Models;

namespace NewSystem_Temp.Data
{
        public class AppDBContext : DbContext
    {
            public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
            {
            }
            public DbSet<Category> Categories { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Category>().HasData(
                    new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                    new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
                    new Category { Id = 3, Name = "History", DisplayOrder = 3 }
                    );
            }
        }
    
}
