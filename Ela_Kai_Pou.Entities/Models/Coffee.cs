using Ela_Kai_Pou.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ela_Kai_Pou.Entities.Models
{
    public class Coffee : Product
    {
        //Properties
        [Required]
        public Sweetness Sweetness { get; set; }

        [Required]
        public Size Size { get; set; }
        
    }
}
