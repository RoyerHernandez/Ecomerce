using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecomerce.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "The field is required")]
        [MaxLength(50, ErrorMessage = "The field could be maximun {1} characters")]
        [Display(Name = "Company")]
        [Index("Company_Name_Index", IsUnique = true)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field is required")]
        [MaxLength(20, ErrorMessage = "The field could be maximun {1} characters")]        
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "The field is required")]
        [MaxLength(100, ErrorMessage = "The field could be maximun {1} characters")]
        public string Address { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Logo { get; set; }

        [Range(1, Double.MaxValue, ErrorMessage = "You must select a {0}")]
        [Required(ErrorMessage = "The field is required")]
        public int DepartmentId { get; set; }

        [Range(1, Double.MaxValue, ErrorMessage = "You must select a {0}")]
        [Required(ErrorMessage = "The field is required")]
        public int CityId { get; set; }

        [NotMapped]
        public HttpPostedFileBase LogoFile { get; set; }

        public virtual City City { get; set; }

        public virtual Deparment Department { get; set; }

    }
}