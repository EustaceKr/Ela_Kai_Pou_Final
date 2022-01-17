using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Ela_Kai_Pou.Entities.Models
{
    public class Cart
    {
        //Properties
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public DateTime Created { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        [Column(TypeName = "money")]
        public decimal TotalPrice { get ; set; }

        //Navigational Properties
        public virtual AppUser User { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        //Constructor
        private Cart()
        {
            OrderItems = new Collection<OrderItem>();
        }

        //Singleton
        private static Cart _instance;

        public static Cart GetNewCart()
        {
            if (_instance == null)
            {
                _instance = new Cart();
            }
            return _instance;
        }

        //Methods

        public decimal GetTotalPrice()
        {
            var total = 0m;
            foreach (var item in OrderItems)
            {
                total += item.TotalPrice;
            }
            return total;
        }
    }
}