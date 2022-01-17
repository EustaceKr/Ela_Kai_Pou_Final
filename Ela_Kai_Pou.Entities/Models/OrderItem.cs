using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ela_Kai_Pou.Entities.Models
{
    public class OrderItem
    {
        //Properties
        [Key]
        public int Id { get; set; }

        [ForeignKey("Product")]
        public int Product_Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal TotalPrice { get { return GetTotalPrice(Product); } }

        [StringLength(150)]
        public string Description { get; set; }

        //Navigational Properties
        public virtual Product Product { get; set; }

        public virtual Order Order { get; set; }

        public virtual Cart Cart { get; set; }

        //Methods

        public decimal GetTotalPrice(Product product)
        {
            return product.Price * Convert.ToDecimal(Quantity);
        }
    }
}
