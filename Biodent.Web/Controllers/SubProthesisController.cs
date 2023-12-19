using Biodent.DataAccess;
using Biodent.Models;
using Biodent.Web.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Biodent.Web.Controllers
{
    public class SubProthesisController : Controller
    {
        SubProthesisDAL _subprothesis;
        public SubProthesisController() 
        {
            _subprothesis = new SubProthesisDAL();
        }

        public IActionResult Index()
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var subpros = _subprothesis.Select();
            return View(subpros);
        }
        public IActionResult Create()
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            SubProthesisModel subpro = new SubProthesisModel();
            ProthesisDAL _prothesis = new ProthesisDAL();
            subpro.Protheses = _prothesis.Select();
            return View(subpro);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(SubProthesisModel subProthesis)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (subProthesis == null)
            {
                return BadRequest();
            }
            else
            {
                _subprothesis.Insert(subProthesis);
            }
            return RedirectToAction("Index");
        }

        public IActionResult ToothPrice()
        {
            var user = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_PublicCurrentUser);
            if (user != null)
            {
                List<SubProthesisModel> subProthesis = new List<SubProthesisModel>();
                subProthesis = _subprothesis.SelectToothPrice();
                return View(subProthesis);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public JsonResult GetPrice(string id)
        {

            var subprothesis = _subprothesis.SelectByID(id);
            decimal? price = subprothesis.SalePrice;
            return Json(price);
        }

        [HttpPost]
        public JsonResult SubProthesisByProID(string id)
        {
            var subprotheses = _subprothesis.SelectByProthesisID(id)
               .Select(sp => new { Value = sp.SubProID, Text = sp.SubProthesisName }).ToList();


            return Json(subprotheses);
        }

    }
}
