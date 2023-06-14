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
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SingularObject> Warehouse { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = System.IO.Path.Combine(Environment.CurrentDirectory, "store.db");
            optionsBuilder.UseSqlite($"Filename={path}");
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<Category>()
                .HasKey(p => p.IdCategory);
            //modelBuilder.Entity<Category>()
            //    .Property(p => p.IdCategory)
            //    .;
            */

            /*
            modelBuilder.Entity<Order>()
                .HasOne(e => e.SingularObject)
                .WithOne(e => e.Order)
                .HasForeignKey<SingularObject>(dkey => dkey.SerialNumber)
                .IsRequired();
            */
            modelBuilder.Entity<SingularObject>()
                .HasOne(e => e.Order)
                .WithOne(e => e.SingularObject)
                .HasForeignKey<Order>(dkey => dkey.SingularObjectId)
                .IsRequired();


            Initialize(modelBuilder);
        }




        private void Initialize(ModelBuilder modelBuilder)
        {
            InitializeCategories(modelBuilder);
        }
        private void InitializeCategories(ModelBuilder modelBuilder)
        {
            Category c1 = new Category { IdCategory =1, Name = "Speakers"};
            Category c2 = new Category { IdCategory =2, Name = "Monitors"};
            Category c3 = new Category { IdCategory =3, Name = "Keyboards"};
            Category c4 = new Category { IdCategory =4, Name = "Microphones"};
            modelBuilder.Entity<Category>()
                .HasData(c1,c2, c3, c4);
        }
        
    }
}
