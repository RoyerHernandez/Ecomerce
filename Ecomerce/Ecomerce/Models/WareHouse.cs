using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecomerce.Models
{
    public class WareHouse
    {
        [Key]
        public int WareHouseId { get; set; }

        [Range(1, Double.MaxValue, ErrorMessage = "You must select a {0}")]
        [Required(ErrorMessage = "The field is required")]
        [Index("WareHouse_Company_WareHouseName_Index", 1, IsUnique = true)]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "The field is required")]
        [MaxLength(50, ErrorMessage = "The field {0} must be maximun {1} characters")]
        [Index("WareHouse_Company_WareHouseName_Index", 2, IsUnique = true)]
        [Display(Name = "WareHouse")]
        public string WareHouseName { get; set; }       

        [Required(ErrorMessage = "The field is required")]
        [MaxLength(20, ErrorMessage = "The field could be maximun {1} characters")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "The field is required")]
        [MaxLength(100, ErrorMessage = "The field could be maximun {1} characters")]
        public string Address { get; set; }

        [Range(1, Double.MaxValue, ErrorMessage = "You must select a {0}")]
        [Required(ErrorMessage = "The field is required")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [Range(1, Double.MaxValue, ErrorMessage = "You must select a {0}")]
        [Required(ErrorMessage = "The field is required")]
        [Display(Name = "City")]
        public int CityId { get; set; }

        public virtual Deparment Department { get; set; }

        public virtual City City { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }
        
    }
}