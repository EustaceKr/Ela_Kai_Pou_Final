using Ela_Kai_Pou.Entities.Enums;
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
    public class Order
    {
        // Properties
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public DateTime? Created { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        [Display(Name ="Order Status")]
        public OrderStatus OrderStatus { get; set; }

        [Display(Name = "Total Price")]
        [Column(TypeName = "money")]
        public decimal TotalPrice { get; set; }

        //Navigational Properties
        public virtual AppUser User { get; set; }

        [Display(Name ="Coffees")]
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        //Constructors
        public Order()
        {
            OrderItems = new Collection<OrderItem>();
        }

        public Order(AppUser user, string paymentMethod)
        {
            User = user;
            Created = DateTime.Now;
            OrderStatus = OrderStatus.Pending;
            OrderItems = new Collection<OrderItem>();
        }
    }
}
