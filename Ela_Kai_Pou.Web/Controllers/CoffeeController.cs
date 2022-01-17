using Ela_Kai_Pou.Entities.Interfaces;
using Ela_Kai_Pou.Entities.Models;
using Ela_Kai_Pou.Web.Areas.Admin.Services;
using Ela_Kai_Pou.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ela_Kai_Pou.Web.Controllers
{
    public class CoffeeController : Controller
    {
        //Properties
        private readonly IUnitOfWork _unitOfWork;

        //Constructor
        public CoffeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Coffee
        public ActionResult Index()
        {
            var products = _unitOfWork.CoffeeRepository.GetActiveProducts();
            var indexVM = ProductService.PopulateIndexVM(products);
            return View(indexVM);
        }
        [Authorize]
        public ActionResult ConfigureProduct(string name)
        {
            if (!User.Identity.IsAuthenticated || name == null)
            {
                return View("Error");
            }
            ViewBag.Name = name;
            TempData["mydata"] = name;
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult ConfigureProduct(OrderItemViewModel model)
        {
            var cart = _unitOfWork.CartRepository.GetCart(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                model.Name = TempData["mydata"] as string;
                Coffee coffee = _unitOfWork.CoffeeRepository.FindCoffee(model.Name, model.Size, model.Sweetness);

                _unitOfWork.OrderItemRepository.GetAll()
                .Where(y => y.Cart.Id == cart.Id)
                .Select(c => c);

                cart = _unitOfWork.CartRepository.AddItem(cart, coffee, model.Quantity);

                _unitOfWork.CartRepository.Update(cart);
                try
                {
                    _unitOfWork.Save();
                }
                catch(Exception ex)
                {
                    return View("Error");
                }
                return RedirectToAction("Index", "Coffee");
            }
            return View();
        }
    }
}