﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecomerce.Models
{
    public class CompanyCustomer
    {
        [Key]
        public int CompanyCustomerId { get; set; }

        public int CompanyId { get; set; }

        public int CustomerId { get; set; }

        public virtual Company Company { get; set; }

        public virtual Customer Cutomer { get; set; }
    }
}