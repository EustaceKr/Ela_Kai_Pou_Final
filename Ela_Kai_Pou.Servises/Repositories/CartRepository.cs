using Ela_Kai_Pou.Entities.Interfaces;
using Ela_Kai_Pou.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ela_Kai_Pou.Servises.Repositories
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(ICoffeeShopDb context) : base(context)
        {

        }

        public Cart GetCart(string id)
        {
            var x = _context.Set<Cart>().Where(o => o.UserId == id).Select(c => c).FirstOrDefault();
            return x;
        }
        public Cart AddItem(Cart cart, Coffee coffee, int quantity = 1)
        {
            OrderItem newItem = cart.OrderItems
            .Where(c => c.Product.Id == coffee.Id)
            .DefaultIfEmpty()
            .FirstOrDefault();

            if (!(newItem == null))
            {
                newItem.Quantity += quantity;
            }
            else
            {
                newItem = new OrderItem();
                newItem.Product = coffee;
                newItem.Quantity = quantity;
                newItem.Description = $"{coffee.Size} / {coffee.Sweetness}";
                cart.OrderItems.Add(newItem);
            }
            return cart;
        }

        public Cart RemoveItem(Cart cart, Coffee coffee)
        {
            OrderItem orderItem = cart.OrderItems.Where(l => l.Product.Id == coffee.Id).FirstOrDefault();
            return cart;
        }

        public Cart RemoveQuantity(Cart cart, Coffee coffee, int quantity)
        {
            OrderItem newItem = cart.OrderItems
            .Where(c => c.Product.Id == coffee.Id)
            .DefaultIfEmpty()
            .FirstOrDefault();

            if (newItem != null && newItem.Quantity > 1)
            {
                newItem.Quantity -= quantity;
            }
            return cart;
        }

        public Cart AddQuantity(Cart cart, Coffee coffee, int quantity)
        {
            OrderItem newItem = cart.OrderItems
            .Where(c => c.Product.Id == coffee.Id)
            .DefaultIfEmpty()
            .FirstOrDefault();

            if (newItem != null && newItem.Quantity > 0)
            {
                newItem.Quantity += quantity;
            }
            return cart;
        }

        public double CartTotalCost(Cart cart)
        {
            return Math.Round(Convert.ToDouble(cart.GetTotalPrice()), 2);
        }

        public void ClearCartItems(Cart cart)
        {
            cart.OrderItems.Clear();
        }

        public int CountItems(Cart cart)
        {
            return cart.OrderItems.Sum(x => x.Quantity);
        }
    }
}
