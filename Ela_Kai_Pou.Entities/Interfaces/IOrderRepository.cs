using Ela_Kai_Pou.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ela_Kai_Pou.Entities.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Order GetOrderFromCart(Cart cart);
        ICollection<Order> GetUserOrders(string id);
        IEnumerable<Order> GetAllByDescenting();
    }
}
