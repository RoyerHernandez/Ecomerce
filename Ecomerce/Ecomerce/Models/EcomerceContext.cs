using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Ecomerce.Models
{
    public class EcomerceContext : DbContext
    {
        public EcomerceContext() : base("DefaultConnection")
        {
            
        }

        public DbSet<Ecomerce.Models.Deparment> Deparments { get; set; }

        public System.Data.Entity.DbSet<Ecomerce.Models.City> Cities { get; set; }
    }
}