using System;
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

        [Range(1, Double.MaxValue, ErrorMessage = "You must select a {0}")]
        [Required(ErrorMessage = "The field is required")]
        public int DepartmentId { get; set; }

        public virtual ICollection<Company> Companies { get; set; }

        public virtual Deparment Department { get; set; }

    }
}