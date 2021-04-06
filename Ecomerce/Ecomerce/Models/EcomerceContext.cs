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

        public System.Data.Entity.DbSet<Ecomerce.Models.City> Cities { get; set; }

        public System.Data.Entity.DbSet<Ecomerce.Models.Company> Companies { get; set; }

        public System.Data.Entity.DbSet<Ecomerce.Models.User> Users { get; set; }
    }
}