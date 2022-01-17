using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ela_Kai_Pou.Entities.Models
{
    public abstract class Product
    {
        //Properties
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        //Navigational Properties
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public bool IsActive { get; set; }

        public bool IsInOrder { get; set; }

        //Constructor

        public Product()
        {
            OrderItems = new HashSet<OrderItem>();
            IsActive = true;
            IsInOrder = false;
        }


    }
}
