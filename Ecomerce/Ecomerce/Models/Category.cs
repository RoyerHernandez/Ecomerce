using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecomerce.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "The field is required")]
        [MaxLength(50, ErrorMessage = "The field could be maximun {1} characters")]
        [Display(Name = "Description")]
        [Index("Category_CompanyId_Description_Index", 2, IsUnique = true)]
        public string Description { get; set; }

        [Range(1, Double.MaxValue, ErrorMessage = "You must select a {0}")]
        [Required(ErrorMessage = "The field is required")]
        [Display(Name = "Company")]
        [Index("Category_CompanyId_Description_Index", 1, IsUnique = true)]
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }
}