using Ela_Kai_Pou.Entities;
using Ela_Kai_Pou.Entities.Interfaces;
using Ela_Kai_Pou.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ela_Kai_Pou.Servises.Repositories
{
    public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(ICoffeeShopDb context) : base(context)
        {

        }
        public void DeleteAllOldCartOrderItems(Cart cart)
        {
            _context.Set<OrderItem>().Where(o => o.Cart.Id == cart.Id).ToList().ForEach(y => Delete(y));

        }
    }
}
