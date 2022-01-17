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
    public class AppUserRepository : GenericRepository<AppUser>, IAppUserRepository
    {
        public AppUser Get(string id)
        {
            return _context.Set<AppUser>().Find(id);
        }

        public AppUserRepository(ICoffeeShopDb context) : base(context)
        {

        }
    }
}
