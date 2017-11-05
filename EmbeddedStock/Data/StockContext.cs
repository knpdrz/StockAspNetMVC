using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using EmbeddedStock.Models;

namespace EmbeddedStock.Data
{
    public class StockContext : DbContext
    {
        public StockContext(DbContextOptions<StockContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<ComponentType> ComponentTypes { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<ComponentTypeCategory> ComponentTypeCategories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<ComponentType>().ToTable("ComponentType");
            modelBuilder.Entity<Component>().ToTable("Component");

            //ComponentTypeCategory has a key combined from ComponentType and Category it joins
            modelBuilder.Entity<ComponentTypeCategory>()
                .HasKey(c => new { c.CategoryID, c.ComponentTypeID });
        }

    }
}
