using Ela_Kai_Pou.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ela_Kai_Pou.Web.Models
{
    public class OrderItemViewModel
    {
        public string Name { get; set; }

        [Required(ErrorMessage ="You need to choose this")]
        public Sweetness Sweetness { get; set; }

        [Required(ErrorMessage = "You need to choose this")]
        public Size Size { get; set; }

        public int Quantity { get; set; }
    }
}