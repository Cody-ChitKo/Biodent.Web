using Biodent.DataAccess;
using Biodent.Models;
using Biodent.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace Biodent.Web.Controllers
{
    public class GenerateNoController : Controller
    {
        GenerateNoDAL _generate;
        public GenerateNoController() 
        {
            _generate = new GenerateNoDAL();
        }
        public IActionResult Index()
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var generates = _generate.GetAll();
            return View(generates);
        }
        public IActionResult Create()
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(GenerateNoModel generate)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            generate.GenerateDate = DateTime.Now.Date;
            _generate.Insert(generate);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var generatedata = _generate.GetById(id);
            return View(generatedata);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(GenerateNoModel generate)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            _generate.Update(generate);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var generatedata = _generate.GetById(id);
            return View(generatedata);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Delete(GenerateNoModel generate)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            _generate.Delete(generate.GenerateID);
            return RedirectToAction("Index");
        }
    }
}
