using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ela_Kai_Pou.Web.Areas.Admin.Models
{
    public class CoffeeViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Single Price")]
        [Range(0.1, 100, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public decimal Price_Single { get; set; }

        [Required]
        [Display(Name = "Double Price")]
        [Range(0.1, 100, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public decimal Price_Double { get; set; }

        [Required]
        [Display(Name = "Quadraple Price")]
        [Range(0.1, 100, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public decimal Price_Quadraple { get; set; }

        public bool IsActive { get; set; }
        public bool IsInOrder { get; set; }
    }
}