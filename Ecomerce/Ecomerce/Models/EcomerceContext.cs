using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Ecomerce.Models
{
    public class EcomerceContext : DbContext
    {
        public EcomerceContext() : base("DefaultConnection")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) 
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public DbSet<Ecomerce.Models.Deparment> Deparments { get; set; }

        public DbSet<Ecomerce.Models.City> Cities { get; set; }

        public DbSet<Ecomerce.Models.Company> Companies { get; set; }

        public DbSet<Ecomerce.Models.User> Users { get; set; }

        public DbSet<Ecomerce.Models.Category> Categories { get; set; }

        public DbSet<Ecomerce.Models.Tax> Taxes { get; set; }

        public DbSet<Ecomerce.Models.Product> Products { get; set; }

        public DbSet<Ecomerce.Models.WareHouse> WareHouses { get; set; }

        public DbSet<Ecomerce.Models.Inventory> Inventories { get; set; }

        public DbSet<Ecomerce.Models.Customer> Customers { get; set; }

        public DbSet<Ecomerce.Models.State> States { get; set; }

        public DbSet<Ecomerce.Models.Order> Orders { get; set; }

        public DbSet<Ecomerce.Models.OrderDetail> OrderDetails { get; set; }

        public DbSet<Ecomerce.Models.OrderDetailTemp> OrderDetailTemps { get; set; }
    }
}