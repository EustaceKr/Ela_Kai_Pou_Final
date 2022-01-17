using Ela_Kai_Pou.Entities.Enums;
using Ela_Kai_Pou.Entities.Interfaces;
using Ela_Kai_Pou.Entities.Models;
using Ela_Kai_Pou.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ela_Kai_Pou.Web.Areas.Admin.Services
{
    public static class ProductService
    {
        public static List<CoffeeViewModel> PopulateIndexVM(List<Coffee> coffeeList)
        {
            var indexVM = new List<CoffeeViewModel>();

            foreach (var coffee in coffeeList)
            {
                var matches = indexVM.Where(x => x.Name == coffee.Name);
                if (matches.Count() == 0)
                {
                    var item = new CoffeeViewModel();
                    item.Name = coffee.Name;
                    item.Description = coffee.Description;
                    item.Id = coffee.Id;
                    item.IsActive = coffee.IsActive;
                    item.IsInOrder = coffee.IsInOrder;
                    indexVM.Add(item);
                }
            }

            foreach (var coffee in coffeeList)
            {
                foreach (var item in indexVM)
                {
                    if (coffee.Name == item.Name && coffee.Size == Size.Single)
                    {
                        item.Price_Single = coffee.Price;
                    }
                    else if (coffee.Name == item.Name && coffee.Size == Size.Double)
                    {
                        item.Price_Double = coffee.Price;
                    }
                    else if (coffee.Name == item.Name && coffee.Size == Size.Quadruple)
                    {
                        item.Price_Quadraple = coffee.Price;
                    }
                }
            }
            return indexVM;
        }

        public static void CreateProducts(CoffeeViewModel coffeeVM, IUnitOfWork unitOfWork)
        {
            foreach (var sweetness in Enum.GetValues(typeof(Sweetness)))
            {
                unitOfWork.CoffeeRepository.Add(
                new Coffee
                {
                    Name = coffeeVM.Name,
                    Price = coffeeVM.Price_Single,
                    Description = coffeeVM.Description,
                    Size = Size.Single,
                    Sweetness = (Sweetness)sweetness
                });
                unitOfWork.CoffeeRepository.Add(
                new Coffee
                {
                    Name = coffeeVM.Name,
                    Price = coffeeVM.Price_Double,
                    Description = coffeeVM.Description,
                    Size = Size.Double,
                    Sweetness = (Sweetness)sweetness
                });
                unitOfWork.CoffeeRepository.Add(
                    new Coffee
                    {
                        Name = coffeeVM.Name,
                        Price = coffeeVM.Price_Quadraple,
                        Description = coffeeVM.Description,
                        Size = Size.Quadruple,
                        Sweetness = (Sweetness)sweetness
                    });
            }
        }
        public static CoffeeViewModel PopulateEditVM(Coffee coffee, IUnitOfWork _unitOfWork)
        {
            var model = new CoffeeViewModel();
            model.Name = coffee.Name;
            model.Description = coffee.Description;
            var list = _unitOfWork.CoffeeRepository.FindCoffeesByName(coffee);
            foreach (var item in list)
            {
                if (item.Size == Size.Single)
                {
                    model.Price_Single = item.Price;
                }
                else if (item.Size == Size.Double)
                {
                    model.Price_Double = item.Price;
                }
                else if (item.Size == Size.Quadruple)
                {
                    model.Price_Quadraple = item.Price;
                }
            }
            return model;
        }

        public static void EditProducts(CoffeeViewModel coffee, IUnitOfWork _unitOfWork)
        {
            var realCoffee = _unitOfWork.CoffeeRepository.Get(coffee.Id);
            var list = _unitOfWork.CoffeeRepository.FindCoffeesByName(realCoffee);

            foreach (var item in list)
            {
                item.Name = coffee.Name;
                item.Description = coffee.Description;
                if (item.Size == Size.Single)
                {
                    item.Price = coffee.Price_Single;
                }
                else if (item.Size == Size.Double)
                {
                    item.Price = coffee.Price_Double;
                }
                else if (item.Size == Size.Quadruple)
                {
                    item.Price = coffee.Price_Quadraple;
                }
                _unitOfWork.CoffeeRepository.Update(item);
            }
        }

        public static void PlaceInOrder(Coffee coffee, IUnitOfWork _unitOfWork)
        {
            var list = _unitOfWork.CoffeeRepository.FindCoffeesByName(coffee);
            foreach (var item in list)
            {
                item.IsInOrder = true;
            }
        }
    }
}