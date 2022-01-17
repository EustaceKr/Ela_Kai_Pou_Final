using Ela_Kai_Pou.Entities;
using Ela_Kai_Pou.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ela_Kai_Pou.Servises.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        //Properties
        private readonly ICoffeeShopDb _context;

        public IOrderRepository OrderRepository { get; }

        public IOrderItemRepository OrderItemRepository { get; }

        public ICoffeeRepository CoffeeRepository { get; }

        public IAppUserRepository AppUserRepository { get; }

        public ICartRepository CartRepository { get; }

        //Constructors
        public UnitOfWork(ICoffeeShopDb context, IOrderRepository orderRepository,
        IOrderItemRepository orderItemRepository, ICoffeeRepository coffeeRepository,
        IAppUserRepository userRepository, ICartRepository cartRepository)
        {
            _context = context;
            OrderRepository = orderRepository;
            OrderItemRepository = orderItemRepository;
            CoffeeRepository = coffeeRepository;
            AppUserRepository = userRepository;
            CartRepository = cartRepository;
        }

        //Dispose implementation
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }


        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
