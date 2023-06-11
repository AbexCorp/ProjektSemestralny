using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using StoreApp.Model;

namespace StoreApp.DataAccess
{
    public class DatabaseContext : DbContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Warehouse> Warehouse { get; set; }
        DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = System.IO.Path.Combine(Environment.CurrentDirectory, "store.db");
            optionsBuilder.UseSqlite(path);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasKey(p => p.IdCategory);
            //modelBuilder.Entity<Category>()
            //    .Property(p => p.IdCategory)
            //    .;
        }
    }
}
