using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ela_Kai_Pou.Entities.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository OrderRepository { get; }
        IOrderItemRepository OrderItemRepository { get; }
        ICoffeeRepository CoffeeRepository { get; }
        IAppUserRepository AppUserRepository { get; }
        ICartRepository CartRepository { get; }
        int Save();
    }
}
