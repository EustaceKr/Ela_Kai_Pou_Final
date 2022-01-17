using Ela_Kai_Pou.Entities.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ela_Kai_Pou.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: Order
        public ActionResult Index()
        {
            return View(_unitOfWork.OrderRepository.GetUserOrders(User.Identity.GetUserId()));
        }
    }
}