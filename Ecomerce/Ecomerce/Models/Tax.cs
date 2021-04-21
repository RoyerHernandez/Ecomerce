using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecomerce.Models
{
    public class Tax
    {
        [Key]
        public int TaxId { get; set; }

        [Required(ErrorMessage = "The field is required")]
        [MaxLength(50, ErrorMessage = "The field could be maximun {1} characters")]
        [Display(Name = "Tax")]
        [Index("Tax_CompanyId_Description_Index", 2, IsUnique = true)]
        public string Description { get; set; }

        [Required(ErrorMessage = "The field is required")]
        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = false)]
        [Range(0.0, 1.0, ErrorMessage = "You must select a values between {1} and {2}")]
        public double Rate { get; set; }

        [Range(1, Double.MaxValue, ErrorMessage = "You must select a {0}")]
        [Required(ErrorMessage = "The field is required")]
        [Display(Name = "Company")]
        [Index("Tax_CompanyId_Description_Index", 1, IsUnique = true)]
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }
}