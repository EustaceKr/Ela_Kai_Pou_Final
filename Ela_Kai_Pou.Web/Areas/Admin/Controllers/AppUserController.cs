using System.Web.Mvc;
using Ela_Kai_Pou.Entities.Interfaces;

namespace Ela_Kai_Pou.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AppUserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppUserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Admin/AppUsers
        public ActionResult Index()
        {
            return View(_unitOfWork.AppUserRepository.GetAll());
        }
    }
}