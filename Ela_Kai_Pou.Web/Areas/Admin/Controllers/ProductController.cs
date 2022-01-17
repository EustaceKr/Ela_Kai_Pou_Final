using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Mvc;
using Ela_Kai_Pou.Entities.Enums;
using Ela_Kai_Pou.Entities.Interfaces;
using Ela_Kai_Pou.Entities.Models;
using Ela_Kai_Pou.Web.Areas.Admin.Models;
using Ela_Kai_Pou.Web.Areas.Admin.Services;

namespace Ela_Kai_Pou.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Admin/Coffees
        public ActionResult Index()
        {
            var products = _unitOfWork.CoffeeRepository.GetAllProducts();
            var indexVM = ProductService.PopulateIndexVM(products);
            return View(indexVM);
        }

        // GET: Admin/Coffees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Coffees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CoffeeViewModel coffeeVM)
        {
            if (ModelState.IsValid)
            {
                ProductService.CreateProducts(coffeeVM, _unitOfWork);
                try
                {
                    _unitOfWork.Save();
                }
                catch (Exception)
                {
                    return View("Error");
                }
                return RedirectToAction("Index");
            }
            return View(coffeeVM);
        }

        [HttpPost]
        public ActionResult DisableProduct(int id)
        {

            Coffee coffee = _unitOfWork.CoffeeRepository.Get(id);
            var active = !coffee.IsActive;
            if (coffee == null)
            {
                return HttpNotFound();
            }

            var list = _unitOfWork.CoffeeRepository.FindCoffeesByName(coffee);
            foreach (var item in list)
            {
                item.IsActive = active;
                _unitOfWork.CoffeeRepository.Update(item);
            }
            try
            {
                _unitOfWork.Save();
            }
            catch(Exception)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        // GET: Admin/Coffees/Edit/5
        public ActionResult Edit(int id)
        {
            Coffee coffee = _unitOfWork.CoffeeRepository.Get(id);
            if (coffee == null)
            {
                return HttpNotFound();
            }
            var model = ProductService.PopulateEditVM(coffee, _unitOfWork);
            return View(model);
        }

        // POST: Admin/Coffees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CoffeeViewModel coffee)
        {
            if (ModelState.IsValid)
            {
                ProductService.EditProducts(coffee, _unitOfWork);
                try
                {
                    _unitOfWork.Save();
                }
                catch
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            return View(coffee);
        }

        // GET: Admin/Coffees/Delete/5
        public ActionResult Delete(int id)
        {
            Coffee coffee = _unitOfWork.CoffeeRepository.Get(id);

            if (coffee == null)
            {
                return HttpNotFound();
            }
            return View(coffee);
        }

        // POST: Admin/Coffees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Coffee coffee)
        {

            Coffee coffee2 = _unitOfWork.CoffeeRepository.Get(coffee.Id);
            var list = _unitOfWork.CoffeeRepository.FindCoffeesByName(coffee2);
            _unitOfWork.CoffeeRepository.DeleteMany(list);
            try
            {
                _unitOfWork.Save();
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}