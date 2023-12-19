using Biodent.DataAccess;
using Biodent.Models;
using Biodent.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace Biodent.Web.Controllers
{
    public class PurchasesController : Controller
    {
        PurchaseDAL _purchase;
        public PurchasesController() 
        {
            _purchase = new PurchaseDAL();
        }
        public IActionResult Index()
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_PublicCurrentUser);

            var purchases = _purchase.GetByUsersID(currentUser.UsersID);
            return View(purchases);
        }
        public IActionResult Payment()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(PurchaseModel purchase)
        {
            return View();
        }
    }
}
