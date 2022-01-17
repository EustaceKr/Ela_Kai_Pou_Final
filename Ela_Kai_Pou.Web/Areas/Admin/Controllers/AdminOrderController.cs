using System;
using System.Linq;
using System.Web.Mvc;
using Ela_Kai_Pou.Entities.Enums;
using Ela_Kai_Pou.Entities.Interfaces;
using Ela_Kai_Pou.Entities.Models;
using Ela_Kai_Pou.Web.Areas.Admin.Models;

namespace Ela_Kai_Pou.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminOrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminOrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Admin/AdminOrder
        public ActionResult Index()
        {

            return View(_unitOfWork.OrderRepository.GetAllByDescenting());
        }

        [HttpPost]
        public ActionResult Edit(int id )
        {
            Order order = _unitOfWork.OrderRepository.Get(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            if(order.OrderStatus == OrderStatus.Pending)
            {
                order.OrderStatus = OrderStatus.Completed;
            }
            else if (order.OrderStatus == OrderStatus.Completed)
                {
                    order.OrderStatus = OrderStatus.Canceled;
                }
            else if (order.OrderStatus == OrderStatus.Canceled)
            {
                order.OrderStatus = OrderStatus.Pending;
            }


            _unitOfWork.OrderRepository.Update(order);
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