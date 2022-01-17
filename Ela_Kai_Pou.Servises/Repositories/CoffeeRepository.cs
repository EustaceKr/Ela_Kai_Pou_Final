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
    public class CoffeeRepository : GenericRepository<Coffee>, ICoffeeRepository
    {
        public CoffeeRepository(ICoffeeShopDb context) : base(context)
        {

        }

        public Coffee FindCoffee(string name, Size size, Sweetness sweetness)
        {
            return _context.Set<Coffee>().Where(p => p.Name == name && p.Size == size && p.Sweetness == sweetness)
            .Select(p => p).FirstOrDefault();
        }

        public IEnumerable<Coffee> FindProductByName(string name)
        {
            return _context.Set<Coffee>().Where(p => p.Name == name).Select(p => p);
        }

        public List<Coffee> GetActiveProducts()
        {
            return _context.Set<Coffee>().Where(x => x.IsActive == true && x.Sweetness == Sweetness.Black).ToList();
        }

        public List<Coffee> GetAllProducts()
        {
            return _context.Set<Coffee>().Where(x => x.Sweetness == Sweetness.Black).ToList();
        }

        public List<Coffee> FindCoffeesByName(Coffee coffee)
        {
            return _context.Set<Coffee>().Where(x => x.Name == coffee.Name).ToList();
        }

        public void DeleteMany(List<Coffee> list)
        {
            foreach (var item in list)
            {
                _context.Set<Coffee>().Remove(item);
            }
        }


    }
}
