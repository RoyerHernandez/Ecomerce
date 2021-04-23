using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecomerce.Models
{
    public class Deparment
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "The field is required")]
        [MaxLength(50, ErrorMessage ="The field could be maximun {1} characters")]
        [Display(Name="Department")]
        [Index("Deparment_Name_Index",IsUnique = true)]
        public string Name { get; set; }

        public virtual  ICollection<City> Cities { get; set; }

        public virtual ICollection<Company> Companies { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<WareHouse> WareHouses { get; set; }
    }
}