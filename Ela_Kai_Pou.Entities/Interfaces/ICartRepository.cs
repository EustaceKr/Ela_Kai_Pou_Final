using Ela_Kai_Pou.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ela_Kai_Pou.Entities.Interfaces
{
    public interface ICartRepository: IGenericRepository<Cart>
    {
        Cart GetCart(string id);
        Cart AddItem(Cart cart, Coffee coffee, int quantity);
        Cart RemoveQuantity(Cart cart, Coffee coffee, int quantity);
        Cart AddQuantity(Cart cart, Coffee coffee, int quantity);
        Cart RemoveItem(Cart cart, Coffee coffee);
        double CartTotalCost(Cart cart);
        void ClearCartItems(Cart cart);
        int CountItems(Cart cart);
    }
}
