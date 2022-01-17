using Ela_Kai_Pou.Entities.Enums;
using Ela_Kai_Pou.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ela_Kai_Pou.Entities.Interfaces
{
    public interface ICoffeeRepository : IGenericRepository<Coffee>
    {
        Coffee FindCoffee(string name, Size size, Sweetness sweetness);
        List<Coffee> GetActiveProducts();
        void DeleteMany(List<Coffee> list);
        List<Coffee> FindCoffeesByName(Coffee coffee);
        List<Coffee> GetAllProducts();
    }
}
