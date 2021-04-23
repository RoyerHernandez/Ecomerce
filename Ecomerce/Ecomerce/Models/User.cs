using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecomerce.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "The field is required")]
        [MaxLength(256, ErrorMessage = "The field {0} must be maximun {1} characters")]
        [Display(Name = "E-Mail")]
        [Index("User_UserName_Index", IsUnique = true)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "The field is required")]
        [MaxLength(50, ErrorMessage = "The field {0} must be maximun {1} characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The field is required")]
        [MaxLength(50, ErrorMessage = "The field {0} must be maximun {1} characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The field is required")]
        [MaxLength(20, ErrorMessage = "The field {0} must be maximun {1} characters")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "The field is required")]
        [MaxLength(100, ErrorMessage = "The field {0} must be maximun {1} characters")]
        public string Address { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Photo { get; set; }

        [NotMapped]
        public HttpPostedFileBase PhotoFile { get; set; }

        [Range(1, Double.MaxValue, ErrorMessage = "You must select a {0}")]
        [Required(ErrorMessage = "The field is required")]
        [Display(Name ="Department")]
        public int DepartmentId { get; set; }

        [Range(1, Double.MaxValue, ErrorMessage = "You must select a {0}")]
        [Required(ErrorMessage = "The field is required")]
        [Display(Name = "City")]
        public int CityId { get; set; }

        [Range(1, Double.MaxValue, ErrorMessage = "You must select a {0}")]
        [Required(ErrorMessage = "The field is required")]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }

        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }       

        public virtual City City { get; set; }

        public virtual Deparment Department { get; set; }

        public virtual Company Company { get; set; }
    }
}