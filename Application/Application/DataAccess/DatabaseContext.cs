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
            InitializeProducts(modelBuilder);
        }
        private void InitializeCategories(ModelBuilder modelBuilder)
        {
            Category c1 = new Category { IdCategory = 1, Name = "Speakers"};
            Category c2 = new Category { IdCategory = 2, Name = "Monitors"};
            Category c3 = new Category { IdCategory = 3, Name = "Keyboards"};
            Category c4 = new Category { IdCategory = 4, Name = "Microphones"};
            modelBuilder.Entity<Category>()
                .HasData(c1,c2, c3, c4);
        }
        private void InitializeProducts(ModelBuilder modelBuilder)
        {
            Category c1 = new Category { IdCategory = 1, Name = "Speakers"};
            Category c2 = new Category { IdCategory = 2, Name = "Monitors"};
            Category c3 = new Category { IdCategory = 3, Name = "Keyboards"};
            Category c4 = new Category { IdCategory = 4, Name = "Microphones"};

            Product p1 = new Product { IdProduct = 1, Name = "Wired wireless apple keyboard"};
            Product p2 = new Product { IdProduct = 2, Name = "Yellow keyboard"};
            Product p3 = new Product { IdProduct = 3, Name = "Generic speaker"};
            Product p4 = new Product { IdProduct = 4, Name = "Razor pro 11 inch"};
            Product p5 = new Product { IdProduct = 5, Name = "Straight edge galaxy fold"};
            Product p6 = new Product { IdProduct = 6, Name = "Blue monkey 30db limited version"};
            modelBuilder.Entity<Product>()
                .HasData
                (
                    new { Name =p1.Name, IdProduct = p1.IdProduct, CategoryId = c3.IdCategory },
                    new { Name =p2.Name, IdProduct = p2.IdProduct, CategoryId = c3.IdCategory },
                    new { Name =p3.Name, IdProduct = p3.IdProduct, CategoryId = c1.IdCategory },
                    new { Name =p4.Name, IdProduct = p4.IdProduct, CategoryId = c2.IdCategory },
                    new { Name =p5.Name, IdProduct = p5.IdProduct, CategoryId = c2.IdCategory },
                    new { Name =p6.Name, IdProduct = p6.IdProduct, CategoryId = c4.IdCategory }
                );
        }
        
    }
}
