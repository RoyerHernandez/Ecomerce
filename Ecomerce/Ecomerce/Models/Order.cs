using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecomerce.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Range(1, Double.MaxValue, ErrorMessage = "You must select a {0}")]
        [Required(ErrorMessage = "The field is required")]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Range(1, double.MaxValue, ErrorMessage = "You must select a {0}")]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Range(1, double.MaxValue, ErrorMessage = "You must select a {0}")]
        [Display(Name = "State")]
        public int StateId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual State State { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    }
}