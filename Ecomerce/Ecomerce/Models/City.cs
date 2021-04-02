﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecomerce.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }

        [Required(ErrorMessage = "The field is required")]
        [MaxLength(50, ErrorMessage = "The field could be maximun {1} characters")]
        [Display(Name="City")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field is required")]
        public int DepartmentId { get; set; }

        public virtual Deparment Department { get; set; }
    }
}