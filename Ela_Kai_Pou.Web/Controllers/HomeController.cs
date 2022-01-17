using Ela_Kai_Pou.Entities.Extensions;
using Ela_Kai_Pou.Entities.Interfaces;
using Ela_Kai_Pou.Entities.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ela_Kai_Pou.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var id = User.Identity.GetUserId();
                var x = _unitOfWork.CartRepository.GetCart(id);
                if (x == null)
                {
                    Cart cart = Cart.GetNewCart();
                    cart.UserId = id;
                    cart.Created = DateTime.Now;
                    _unitOfWork.CartRepository.Add(cart);
                    try
                    {
                        _unitOfWork.Save();
                    }
                    catch (Exception)
                    {
                        return View("Error");
                    }
                }
            }
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Chat()
        {            
            return View();
        }
    }
}