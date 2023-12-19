using Biodent.DataAccess;
using Biodent.Models;
using Biodent.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace Biodent.Web.Controllers
{
    public class DashBoardController : Controller
    {
        DashBoardDAL _dashboard;

        public DashBoardController() 
        {
            _dashboard = new DashBoardDAL();
        }
        public IActionResult Index()
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var dashboards = _dashboard.DashView(DateTime.Now.AddDays(-DateTime.Now.Day), DateTime.Now.Date);
            return View(dashboards);
        }
    }
}
