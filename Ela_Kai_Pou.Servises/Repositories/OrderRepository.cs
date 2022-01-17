using Ela_Kai_Pou.Entities;
using Ela_Kai_Pou.Entities.Enums;
using Ela_Kai_Pou.Entities.Interfaces;
using Ela_Kai_Pou.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ela_Kai_Pou.Servises.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ICoffeeShopDb context) : base(context)
        {

        }

        public Order GetOrderFromCart(Cart cart)
        {
            Order order = new Order();
            foreach (var item in cart.OrderItems)
            {
                order.OrderItems.Add(item);
            }
            order.TotalPrice = cart.TotalPrice;
            order.UserId = cart.UserId;
            order.Created = DateTime.UtcNow;

            return order;
        }

        public ICollection<Order> GetUserOrders(string id)
        {
            return _context.Set<Order>().Where(x => x.UserId == id && x.OrderStatus != OrderStatus.Canceled).OrderByDescending(x => x.Created.Value).ToList();
        }

        public IEnumerable<Order> GetAllByDescenting()
        {
            return _context.Set<Order>().OrderByDescending(x => x.Created.Value).ToList();
        }

        
    }
}
